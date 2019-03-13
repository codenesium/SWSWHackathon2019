using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public abstract class AbstractEmailService : AbstractService
	{
		private MediatR.IMediator mediator;

		protected IEmailRepository EmailRepository { get; private set; }

		protected IApiEmailServerRequestModelValidator EmailModelValidator { get; private set; }

		protected IDALEmailMapper DalEmailMapper { get; private set; }

		private ILogger logger;

		public AbstractEmailService(
			ILogger logger,
			MediatR.IMediator mediator,
			IEmailRepository emailRepository,
			IApiEmailServerRequestModelValidator emailModelValidator,
			IDALEmailMapper dalEmailMapper)
			: base()
		{
			this.EmailRepository = emailRepository;
			this.EmailModelValidator = emailModelValidator;
			this.DalEmailMapper = dalEmailMapper;
			this.logger = logger;

			this.mediator = mediator;
		}

		public virtual async Task<List<ApiEmailServerResponseModel>> All(int limit = 0, int offset = int.MaxValue, string query = "")
		{
			List<Email> records = await this.EmailRepository.All(limit, offset, query);

			return this.DalEmailMapper.MapEntityToModel(records);
		}

		public virtual async Task<ApiEmailServerResponseModel> Get(int id)
		{
			Email record = await this.EmailRepository.Get(id);

			if (record == null)
			{
				return null;
			}
			else
			{
				return this.DalEmailMapper.MapEntityToModel(record);
			}
		}

		public virtual async Task<CreateResponse<ApiEmailServerResponseModel>> Create(
			ApiEmailServerRequestModel model)
		{
			CreateResponse<ApiEmailServerResponseModel> response = ValidationResponseFactory<ApiEmailServerResponseModel>.CreateResponse(await this.EmailModelValidator.ValidateCreateAsync(model));

			if (response.Success)
			{
				Email record = this.DalEmailMapper.MapModelToEntity(default(int), model);
				record = await this.EmailRepository.Create(record);

				response.SetRecord(this.DalEmailMapper.MapEntityToModel(record));
				await this.mediator.Publish(new EmailCreatedNotification(response.Record));
			}

			return response;
		}

		public virtual async Task<UpdateResponse<ApiEmailServerResponseModel>> Update(
			int id,
			ApiEmailServerRequestModel model)
		{
			var validationResult = await this.EmailModelValidator.ValidateUpdateAsync(id, model);

			if (validationResult.IsValid)
			{
				Email record = this.DalEmailMapper.MapModelToEntity(id, model);
				await this.EmailRepository.Update(record);

				record = await this.EmailRepository.Get(id);

				ApiEmailServerResponseModel apiModel = this.DalEmailMapper.MapEntityToModel(record);
				await this.mediator.Publish(new EmailUpdatedNotification(apiModel));

				return ValidationResponseFactory<ApiEmailServerResponseModel>.UpdateResponse(apiModel);
			}
			else
			{
				return ValidationResponseFactory<ApiEmailServerResponseModel>.UpdateResponse(validationResult);
			}
		}

		public virtual async Task<ActionResponse> Delete(
			int id)
		{
			ActionResponse response = ValidationResponseFactory<object>.ActionResponse(await this.EmailModelValidator.ValidateDeleteAsync(id));

			if (response.Success)
			{
				await this.EmailRepository.Delete(id);

				await this.mediator.Publish(new EmailDeletedNotification(id));
			}

			return response;
		}

		public async virtual Task<List<ApiEmailServerResponseModel>> ByArtistId(int artistId, int limit = 0, int offset = int.MaxValue)
		{
			List<Email> records = await this.EmailRepository.ByArtistId(artistId, limit, offset);

			return this.DalEmailMapper.MapEntityToModel(records);
		}
	}
}

/*<Codenesium>
    <Hash>c6261b7e8b738daeb9b81a3bf26f7f58</Hash>
</Codenesium>*/