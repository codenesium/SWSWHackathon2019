using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public abstract class AbstractArtistService : AbstractService
	{
		private MediatR.IMediator mediator;

		protected IArtistRepository ArtistRepository { get; private set; }

		protected IApiArtistServerRequestModelValidator ArtistModelValidator { get; private set; }

		protected IDALArtistMapper DalArtistMapper { get; private set; }

		protected IDALBankAccountMapper DalBankAccountMapper { get; private set; }

		protected IDALEmailMapper DalEmailMapper { get; private set; }

		protected IDALTransactionMapper DalTransactionMapper { get; private set; }

		private ILogger logger;

		public AbstractArtistService(
			ILogger logger,
			MediatR.IMediator mediator,
			IArtistRepository artistRepository,
			IApiArtistServerRequestModelValidator artistModelValidator,
			IDALArtistMapper dalArtistMapper,
			IDALBankAccountMapper dalBankAccountMapper,
			IDALEmailMapper dalEmailMapper,
			IDALTransactionMapper dalTransactionMapper)
			: base()
		{
			this.ArtistRepository = artistRepository;
			this.ArtistModelValidator = artistModelValidator;
			this.DalArtistMapper = dalArtistMapper;
			this.DalBankAccountMapper = dalBankAccountMapper;
			this.DalEmailMapper = dalEmailMapper;
			this.DalTransactionMapper = dalTransactionMapper;
			this.logger = logger;

			this.mediator = mediator;
		}

		public virtual async Task<List<ApiArtistServerResponseModel>> All(int limit = 0, int offset = int.MaxValue, string query = "")
		{
			List<Artist> records = await this.ArtistRepository.All(limit, offset, query);

			return this.DalArtistMapper.MapEntityToModel(records);
		}

		public virtual async Task<ApiArtistServerResponseModel> Get(int id)
		{
			Artist record = await this.ArtistRepository.Get(id);

			if (record == null)
			{
				return null;
			}
			else
			{
				return this.DalArtistMapper.MapEntityToModel(record);
			}
		}

		public virtual async Task<CreateResponse<ApiArtistServerResponseModel>> Create(
			ApiArtistServerRequestModel model)
		{
			CreateResponse<ApiArtistServerResponseModel> response = ValidationResponseFactory<ApiArtistServerResponseModel>.CreateResponse(await this.ArtistModelValidator.ValidateCreateAsync(model));

			if (response.Success)
			{
				Artist record = this.DalArtistMapper.MapModelToEntity(default(int), model);
				record = await this.ArtistRepository.Create(record);

				response.SetRecord(this.DalArtistMapper.MapEntityToModel(record));
				await this.mediator.Publish(new ArtistCreatedNotification(response.Record));
			}

			return response;
		}

		public virtual async Task<UpdateResponse<ApiArtistServerResponseModel>> Update(
			int id,
			ApiArtistServerRequestModel model)
		{
			var validationResult = await this.ArtistModelValidator.ValidateUpdateAsync(id, model);

			if (validationResult.IsValid)
			{
				Artist record = this.DalArtistMapper.MapModelToEntity(id, model);
				await this.ArtistRepository.Update(record);

				record = await this.ArtistRepository.Get(id);

				ApiArtistServerResponseModel apiModel = this.DalArtistMapper.MapEntityToModel(record);
				await this.mediator.Publish(new ArtistUpdatedNotification(apiModel));

				return ValidationResponseFactory<ApiArtistServerResponseModel>.UpdateResponse(apiModel);
			}
			else
			{
				return ValidationResponseFactory<ApiArtistServerResponseModel>.UpdateResponse(validationResult);
			}
		}

		public virtual async Task<ActionResponse> Delete(
			int id)
		{
			ActionResponse response = ValidationResponseFactory<object>.ActionResponse(await this.ArtistModelValidator.ValidateDeleteAsync(id));

			if (response.Success)
			{
				await this.ArtistRepository.Delete(id);

				await this.mediator.Publish(new ArtistDeletedNotification(id));
			}

			return response;
		}

		public async virtual Task<List<ApiBankAccountServerResponseModel>> BankAccountsByArtistId(int artistId, int limit = int.MaxValue, int offset = 0)
		{
			List<BankAccount> records = await this.ArtistRepository.BankAccountsByArtistId(artistId, limit, offset);

			return this.DalBankAccountMapper.MapEntityToModel(records);
		}

		public async virtual Task<List<ApiEmailServerResponseModel>> EmailsByArtistId(int artistId, int limit = int.MaxValue, int offset = 0)
		{
			List<Email> records = await this.ArtistRepository.EmailsByArtistId(artistId, limit, offset);

			return this.DalEmailMapper.MapEntityToModel(records);
		}

		public async virtual Task<List<ApiTransactionServerResponseModel>> TransactionsByArtistId(int artistId, int limit = int.MaxValue, int offset = 0)
		{
			List<Transaction> records = await this.ArtistRepository.TransactionsByArtistId(artistId, limit, offset);

			return this.DalTransactionMapper.MapEntityToModel(records);
		}
	}
}

/*<Codenesium>
    <Hash>70f9d8923338c5a10593b40cdb71b06b</Hash>
</Codenesium>*/