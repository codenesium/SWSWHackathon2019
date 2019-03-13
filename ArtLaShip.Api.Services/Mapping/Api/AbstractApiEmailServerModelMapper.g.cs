using ArtLaShipNS.Api.Client;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public abstract class AbstractApiEmailServerModelMapper
	{
		public virtual ApiEmailServerResponseModel MapServerRequestToResponse(
			int id,
			ApiEmailServerRequestModel request)
		{
			var response = new ApiEmailServerResponseModel();
			response.SetProperties(id,
			                       request.ArtistId,
			                       request.DateCreated,
			                       request.EmailValue);
			return response;
		}

		public virtual ApiEmailServerRequestModel MapServerResponseToRequest(
			ApiEmailServerResponseModel response)
		{
			var request = new ApiEmailServerRequestModel();
			request.SetProperties(
				response.ArtistId,
				response.DateCreated,
				response.EmailValue);
			return request;
		}

		public virtual ApiEmailClientRequestModel MapServerResponseToClientRequest(
			ApiEmailServerResponseModel response)
		{
			var request = new ApiEmailClientRequestModel();
			request.SetProperties(
				response.ArtistId,
				response.DateCreated,
				response.EmailValue);
			return request;
		}

		public JsonPatchDocument<ApiEmailServerRequestModel> CreatePatch(ApiEmailServerRequestModel model)
		{
			var patch = new JsonPatchDocument<ApiEmailServerRequestModel>();
			patch.Replace(x => x.ArtistId, model.ArtistId);
			patch.Replace(x => x.DateCreated, model.DateCreated);
			patch.Replace(x => x.EmailValue, model.EmailValue);
			return patch;
		}
	}
}

/*<Codenesium>
    <Hash>a6e17c8eebfaad7b62cde0d0d1266319</Hash>
</Codenesium>*/