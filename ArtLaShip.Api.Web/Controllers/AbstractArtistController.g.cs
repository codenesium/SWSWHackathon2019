using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.Services;
using Codenesium.Foundation.CommonMVC;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Web
{
	public abstract class AbstractArtistController : AbstractApiController
	{
		protected IArtistService ArtistService { get; private set; }

		protected IApiArtistServerModelMapper ArtistModelMapper { get; private set; }

		protected int BulkInsertLimit { get; set; }

		protected int MaxLimit { get; set; }

		protected int DefaultLimit { get; set; }

		public AbstractArtistController(
			ApiSettings settings,
			ILogger<AbstractArtistController> logger,
			ITransactionCoordinator transactionCoordinator,
			IArtistService artistService,
			IApiArtistServerModelMapper artistModelMapper
			)
			: base(settings, logger, transactionCoordinator)
		{
			this.ArtistService = artistService;
			this.ArtistModelMapper = artistModelMapper;
		}

		[HttpGet]
		[Route("")]
		[ReadOnly]
		[ProducesResponseType(typeof(List<ApiArtistServerResponseModel>), 200)]

		public async virtual Task<IActionResult> All(int? limit, int? offset, string query)
		{
			SearchQuery searchQuery = new SearchQuery();
			if (!searchQuery.Process(this.MaxLimit, this.DefaultLimit, limit, offset, query, this.ControllerContext.HttpContext.Request.Query.ToDictionary(q => q.Key, q => q.Value)))
			{
				return this.StatusCode(StatusCodes.Status413PayloadTooLarge, searchQuery.Error);
			}

			List<ApiArtistServerResponseModel> response = await this.ArtistService.All(searchQuery.Limit, searchQuery.Offset, searchQuery.Query);

			return this.Ok(response);
		}

		[HttpGet]
		[Route("{id}")]
		[ReadOnly]
		[ProducesResponseType(typeof(ApiArtistServerResponseModel), 200)]
		[ProducesResponseType(typeof(void), 404)]

		public async virtual Task<IActionResult> Get(int id)
		{
			ApiArtistServerResponseModel response = await this.ArtistService.Get(id);

			if (response == null)
			{
				return this.StatusCode(StatusCodes.Status404NotFound);
			}
			else
			{
				return this.Ok(response);
			}
		}

		[HttpPost]
		[Route("BulkInsert")]
		[UnitOfWork]
		[ProducesResponseType(typeof(CreateResponse<List<ApiArtistServerResponseModel>>), 200)]
		[ProducesResponseType(typeof(void), 413)]
		[ProducesResponseType(typeof(ActionResponse), 422)]

		public virtual async Task<IActionResult> BulkInsert([FromBody] List<ApiArtistServerRequestModel> models)
		{
			if (models.Count > this.BulkInsertLimit)
			{
				return this.StatusCode(StatusCodes.Status413PayloadTooLarge);
			}

			List<ApiArtistServerResponseModel> records = new List<ApiArtistServerResponseModel>();
			foreach (var model in models)
			{
				CreateResponse<ApiArtistServerResponseModel> result = await this.ArtistService.Create(model);

				if (result.Success)
				{
					records.Add(result.Record);
				}
				else
				{
					return this.StatusCode(StatusCodes.Status422UnprocessableEntity, result);
				}
			}

			var response = new CreateResponse<List<ApiArtistServerResponseModel>>();
			response.SetRecord(records);

			return this.Ok(response);
		}

		[HttpPost]
		[Route("")]
		[UnitOfWork]
		[ProducesResponseType(typeof(CreateResponse<ApiArtistServerResponseModel>), 201)]
		[ProducesResponseType(typeof(ActionResponse), 422)]

		public virtual async Task<IActionResult> Create([FromBody] ApiArtistServerRequestModel model)
		{
			CreateResponse<ApiArtistServerResponseModel> result = await this.ArtistService.Create(model);

			if (result.Success)
			{
				return this.Created($"{this.Settings.ExternalBaseUrl}/api/Artists/{result.Record.Id}", result);
			}
			else
			{
				return this.StatusCode(StatusCodes.Status422UnprocessableEntity, result);
			}
		}

		[HttpPatch]
		[Route("{id}")]
		[UnitOfWork]
		[ProducesResponseType(typeof(UpdateResponse<ApiArtistServerResponseModel>), 200)]
		[ProducesResponseType(typeof(void), 404)]
		[ProducesResponseType(typeof(ActionResponse), 422)]

		public virtual async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<ApiArtistServerRequestModel> patch)
		{
			ApiArtistServerResponseModel record = await this.ArtistService.Get(id);

			if (record == null)
			{
				return this.StatusCode(StatusCodes.Status404NotFound);
			}
			else
			{
				ApiArtistServerRequestModel model = await this.PatchModel(id, patch) as ApiArtistServerRequestModel;

				UpdateResponse<ApiArtistServerResponseModel> result = await this.ArtistService.Update(id, model);

				if (result.Success)
				{
					return this.Ok(result);
				}
				else
				{
					return this.StatusCode(StatusCodes.Status422UnprocessableEntity, result);
				}
			}
		}

		[HttpPut]
		[Route("{id}")]
		[UnitOfWork]
		[ProducesResponseType(typeof(UpdateResponse<ApiArtistServerResponseModel>), 200)]
		[ProducesResponseType(typeof(void), 404)]
		[ProducesResponseType(typeof(ActionResponse), 422)]

		public virtual async Task<IActionResult> Update(int id, [FromBody] ApiArtistServerRequestModel model)
		{
			ApiArtistServerRequestModel request = await this.PatchModel(id, this.ArtistModelMapper.CreatePatch(model)) as ApiArtistServerRequestModel;

			if (request == null)
			{
				return this.StatusCode(StatusCodes.Status404NotFound);
			}
			else
			{
				UpdateResponse<ApiArtistServerResponseModel> result = await this.ArtistService.Update(id, request);

				if (result.Success)
				{
					return this.Ok(result);
				}
				else
				{
					return this.StatusCode(StatusCodes.Status422UnprocessableEntity, result);
				}
			}
		}

		[HttpDelete]
		[Route("{id}")]
		[UnitOfWork]
		[ProducesResponseType(typeof(ActionResponse), 200)]
		[ProducesResponseType(typeof(ActionResponse), 422)]

		public virtual async Task<IActionResult> Delete(int id)
		{
			ActionResponse result = await this.ArtistService.Delete(id);

			if (result.Success)
			{
				return this.StatusCode(StatusCodes.Status200OK, result);
			}
			else
			{
				return this.StatusCode(StatusCodes.Status422UnprocessableEntity, result);
			}
		}

		[HttpGet]
		[Route("{artistId}/BankAccounts")]
		[ReadOnly]
		[ProducesResponseType(typeof(List<ApiBankAccountServerResponseModel>), 200)]
		public async virtual Task<IActionResult> BankAccountsByArtistId(int artistId, int? limit, int? offset)
		{
			SearchQuery query = new SearchQuery();
			if (!query.Process(this.MaxLimit, this.DefaultLimit, limit, offset, string.Empty, this.ControllerContext.HttpContext.Request.Query.ToDictionary(q => q.Key, q => q.Value)))
			{
				return this.StatusCode(StatusCodes.Status413PayloadTooLarge, query.Error);
			}

			List<ApiBankAccountServerResponseModel> response = await this.ArtistService.BankAccountsByArtistId(artistId, query.Limit, query.Offset);

			return this.Ok(response);
		}

		[HttpGet]
		[Route("{artistId}/Emails")]
		[ReadOnly]
		[ProducesResponseType(typeof(List<ApiEmailServerResponseModel>), 200)]
		public async virtual Task<IActionResult> EmailsByArtistId(int artistId, int? limit, int? offset)
		{
			SearchQuery query = new SearchQuery();
			if (!query.Process(this.MaxLimit, this.DefaultLimit, limit, offset, string.Empty, this.ControllerContext.HttpContext.Request.Query.ToDictionary(q => q.Key, q => q.Value)))
			{
				return this.StatusCode(StatusCodes.Status413PayloadTooLarge, query.Error);
			}

			List<ApiEmailServerResponseModel> response = await this.ArtistService.EmailsByArtistId(artistId, query.Limit, query.Offset);

			return this.Ok(response);
		}

		[HttpGet]
		[Route("{artistId}/Transactions")]
		[ReadOnly]
		[ProducesResponseType(typeof(List<ApiTransactionServerResponseModel>), 200)]
		public async virtual Task<IActionResult> TransactionsByArtistId(int artistId, int? limit, int? offset)
		{
			SearchQuery query = new SearchQuery();
			if (!query.Process(this.MaxLimit, this.DefaultLimit, limit, offset, string.Empty, this.ControllerContext.HttpContext.Request.Query.ToDictionary(q => q.Key, q => q.Value)))
			{
				return this.StatusCode(StatusCodes.Status413PayloadTooLarge, query.Error);
			}

			List<ApiTransactionServerResponseModel> response = await this.ArtistService.TransactionsByArtistId(artistId, query.Limit, query.Offset);

			return this.Ok(response);
		}

		private async Task<ApiArtistServerRequestModel> PatchModel(int id, JsonPatchDocument<ApiArtistServerRequestModel> patch)
		{
			var record = await this.ArtistService.Get(id);

			if (record == null)
			{
				return null;
			}
			else
			{
				ApiArtistServerRequestModel request = this.ArtistModelMapper.MapServerResponseToRequest(record);
				patch.ApplyTo(request);
				return request;
			}
		}
	}
}

/*<Codenesium>
    <Hash>7af83cd12f1a21763335b2b4121f0d37</Hash>
</Codenesium>*/