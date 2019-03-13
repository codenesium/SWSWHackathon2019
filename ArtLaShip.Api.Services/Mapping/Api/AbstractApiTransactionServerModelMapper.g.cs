using ArtLaShipNS.Api.Client;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public abstract class AbstractApiTransactionServerModelMapper
	{
		public virtual ApiTransactionServerResponseModel MapServerRequestToResponse(
			int id,
			ApiTransactionServerRequestModel request)
		{
			var response = new ApiTransactionServerResponseModel();
			response.SetProperties(id,
			                       request.Amount,
			                       request.ArtistId,
			                       request.DateCreated,
			                       request.StripeTransactionId);
			return response;
		}

		public virtual ApiTransactionServerRequestModel MapServerResponseToRequest(
			ApiTransactionServerResponseModel response)
		{
			var request = new ApiTransactionServerRequestModel();
			request.SetProperties(
				response.Amount,
				response.ArtistId,
				response.DateCreated,
				response.StripeTransactionId);
			return request;
		}

		public virtual ApiTransactionClientRequestModel MapServerResponseToClientRequest(
			ApiTransactionServerResponseModel response)
		{
			var request = new ApiTransactionClientRequestModel();
			request.SetProperties(
				response.Amount,
				response.ArtistId,
				response.DateCreated,
				response.StripeTransactionId);
			return request;
		}

		public JsonPatchDocument<ApiTransactionServerRequestModel> CreatePatch(ApiTransactionServerRequestModel model)
		{
			var patch = new JsonPatchDocument<ApiTransactionServerRequestModel>();
			patch.Replace(x => x.Amount, model.Amount);
			patch.Replace(x => x.ArtistId, model.ArtistId);
			patch.Replace(x => x.DateCreated, model.DateCreated);
			patch.Replace(x => x.StripeTransactionId, model.StripeTransactionId);
			return patch;
		}
	}
}

/*<Codenesium>
    <Hash>20b6ddd9d9ace2630f503a53ccb9c4ef</Hash>
</Codenesium>*/