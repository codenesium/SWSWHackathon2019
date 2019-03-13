using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public abstract class AbstractBankAccountService : AbstractService
	{
		private MediatR.IMediator mediator;

		protected IBankAccountRepository BankAccountRepository { get; private set; }

		protected IApiBankAccountServerRequestModelValidator BankAccountModelValidator { get; private set; }

		protected IDALBankAccountMapper DalBankAccountMapper { get; private set; }

		private ILogger logger;

		public AbstractBankAccountService(
			ILogger logger,
			MediatR.IMediator mediator,
			IBankAccountRepository bankAccountRepository,
			IApiBankAccountServerRequestModelValidator bankAccountModelValidator,
			IDALBankAccountMapper dalBankAccountMapper)
			: base()
		{
			this.BankAccountRepository = bankAccountRepository;
			this.BankAccountModelValidator = bankAccountModelValidator;
			this.DalBankAccountMapper = dalBankAccountMapper;
			this.logger = logger;

			this.mediator = mediator;
		}

		public virtual async Task<List<ApiBankAccountServerResponseModel>> All(int limit = 0, int offset = int.MaxValue, string query = "")
		{
			List<BankAccount> records = await this.BankAccountRepository.All(limit, offset, query);

			return this.DalBankAccountMapper.MapEntityToModel(records);
		}

		public virtual async Task<ApiBankAccountServerResponseModel> Get(int id)
		{
			BankAccount record = await this.BankAccountRepository.Get(id);

			if (record == null)
			{
				return null;
			}
			else
			{
				return this.DalBankAccountMapper.MapEntityToModel(record);
			}
		}

		public virtual async Task<CreateResponse<ApiBankAccountServerResponseModel>> Create(
			ApiBankAccountServerRequestModel model)
		{
			CreateResponse<ApiBankAccountServerResponseModel> response = ValidationResponseFactory<ApiBankAccountServerResponseModel>.CreateResponse(await this.BankAccountModelValidator.ValidateCreateAsync(model));

			if (response.Success)
			{
				BankAccount record = this.DalBankAccountMapper.MapModelToEntity(default(int), model);
				record = await this.BankAccountRepository.Create(record);

				response.SetRecord(this.DalBankAccountMapper.MapEntityToModel(record));
				await this.mediator.Publish(new BankAccountCreatedNotification(response.Record));
			}

			return response;
		}

		public virtual async Task<UpdateResponse<ApiBankAccountServerResponseModel>> Update(
			int id,
			ApiBankAccountServerRequestModel model)
		{
			var validationResult = await this.BankAccountModelValidator.ValidateUpdateAsync(id, model);

			if (validationResult.IsValid)
			{
				BankAccount record = this.DalBankAccountMapper.MapModelToEntity(id, model);
				await this.BankAccountRepository.Update(record);

				record = await this.BankAccountRepository.Get(id);

				ApiBankAccountServerResponseModel apiModel = this.DalBankAccountMapper.MapEntityToModel(record);
				await this.mediator.Publish(new BankAccountUpdatedNotification(apiModel));

				return ValidationResponseFactory<ApiBankAccountServerResponseModel>.UpdateResponse(apiModel);
			}
			else
			{
				return ValidationResponseFactory<ApiBankAccountServerResponseModel>.UpdateResponse(validationResult);
			}
		}

		public virtual async Task<ActionResponse> Delete(
			int id)
		{
			ActionResponse response = ValidationResponseFactory<object>.ActionResponse(await this.BankAccountModelValidator.ValidateDeleteAsync(id));

			if (response.Success)
			{
				await this.BankAccountRepository.Delete(id);

				await this.mediator.Publish(new BankAccountDeletedNotification(id));
			}

			return response;
		}

		public async virtual Task<List<ApiBankAccountServerResponseModel>> ByArtistId(int artistId, int limit = 0, int offset = int.MaxValue)
		{
			List<BankAccount> records = await this.BankAccountRepository.ByArtistId(artistId, limit, offset);

			return this.DalBankAccountMapper.MapEntityToModel(records);
		}
	}
}

/*<Codenesium>
    <Hash>22c88e910f8bf4645d6691021332d4f2</Hash>
</Codenesium>*/