using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public abstract class AbstractTransactionService : AbstractService
	{
		private MediatR.IMediator mediator;

		protected ITransactionRepository TransactionRepository { get; private set; }

		protected IApiTransactionServerRequestModelValidator TransactionModelValidator { get; private set; }

		protected IDALTransactionMapper DalTransactionMapper { get; private set; }

		private ILogger logger;

		public AbstractTransactionService(
			ILogger logger,
			MediatR.IMediator mediator,
			ITransactionRepository transactionRepository,
			IApiTransactionServerRequestModelValidator transactionModelValidator,
			IDALTransactionMapper dalTransactionMapper)
			: base()
		{
			this.TransactionRepository = transactionRepository;
			this.TransactionModelValidator = transactionModelValidator;
			this.DalTransactionMapper = dalTransactionMapper;
			this.logger = logger;

			this.mediator = mediator;
		}

		public virtual async Task<List<ApiTransactionServerResponseModel>> All(int limit = 0, int offset = int.MaxValue, string query = "")
		{
			List<Transaction> records = await this.TransactionRepository.All(limit, offset, query);

			return this.DalTransactionMapper.MapEntityToModel(records);
		}

		public virtual async Task<ApiTransactionServerResponseModel> Get(int id)
		{
			Transaction record = await this.TransactionRepository.Get(id);

			if (record == null)
			{
				return null;
			}
			else
			{
				return this.DalTransactionMapper.MapEntityToModel(record);
			}
		}

		public virtual async Task<CreateResponse<ApiTransactionServerResponseModel>> Create(
			ApiTransactionServerRequestModel model)
		{
			CreateResponse<ApiTransactionServerResponseModel> response = ValidationResponseFactory<ApiTransactionServerResponseModel>.CreateResponse(await this.TransactionModelValidator.ValidateCreateAsync(model));

			if (response.Success)
			{
				Transaction record = this.DalTransactionMapper.MapModelToEntity(default(int), model);
				record = await this.TransactionRepository.Create(record);

				response.SetRecord(this.DalTransactionMapper.MapEntityToModel(record));
				await this.mediator.Publish(new TransactionCreatedNotification(response.Record));
			}

			return response;
		}

		public virtual async Task<UpdateResponse<ApiTransactionServerResponseModel>> Update(
			int id,
			ApiTransactionServerRequestModel model)
		{
			var validationResult = await this.TransactionModelValidator.ValidateUpdateAsync(id, model);

			if (validationResult.IsValid)
			{
				Transaction record = this.DalTransactionMapper.MapModelToEntity(id, model);
				await this.TransactionRepository.Update(record);

				record = await this.TransactionRepository.Get(id);

				ApiTransactionServerResponseModel apiModel = this.DalTransactionMapper.MapEntityToModel(record);
				await this.mediator.Publish(new TransactionUpdatedNotification(apiModel));

				return ValidationResponseFactory<ApiTransactionServerResponseModel>.UpdateResponse(apiModel);
			}
			else
			{
				return ValidationResponseFactory<ApiTransactionServerResponseModel>.UpdateResponse(validationResult);
			}
		}

		public virtual async Task<ActionResponse> Delete(
			int id)
		{
			ActionResponse response = ValidationResponseFactory<object>.ActionResponse(await this.TransactionModelValidator.ValidateDeleteAsync(id));

			if (response.Success)
			{
				await this.TransactionRepository.Delete(id);

				await this.mediator.Publish(new TransactionDeletedNotification(id));
			}

			return response;
		}

		public async virtual Task<List<ApiTransactionServerResponseModel>> ByArtistId(int artistId, int limit = 0, int offset = int.MaxValue)
		{
			List<Transaction> records = await this.TransactionRepository.ByArtistId(artistId, limit, offset);

			return this.DalTransactionMapper.MapEntityToModel(records);
		}
	}
}

/*<Codenesium>
    <Hash>d4bbd582ba342fe37a16cabc874cd258</Hash>
</Codenesium>*/