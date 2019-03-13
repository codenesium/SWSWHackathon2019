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
			                       request.Email1);
			return response;
		}

		public virtual ApiEmailServerRequestModel MapServerResponseToRequest(
			ApiEmailServerResponseModel response)
		{
			var request = new ApiEmailServerRequestModel();
			request.SetProperties(
				response.ArtistId,
				response.DateCreated,
				response.Email1);
			return request;
		}

		public virtual ApiEmailClientRequestModel MapServerResponseToClientRequest(
			ApiEmailServerResponseModel response)
		{
			var request = new ApiEmailClientRequestModel();
			request.SetProperties(
				response.ArtistId,
				response.DateCreated,
				response.Email1);
			return request;
		}

		public JsonPatchDocument<ApiEmailServerRequestModel> CreatePatch(ApiEmailServerRequestModel model)
		{
			var patch = new JsonPatchDocument<ApiEmailServerRequestModel>();
			patch.Replace(x => x.ArtistId, model.ArtistId);
			patch.Replace(x => x.DateCreated, model.DateCreated);
			patch.Replace(x => x.Email1, model.Email1);
			return patch;
		}
	}
}

/*<Codenesium>
    <Hash>e1fb316acbdaa7b9ecb439daa3c825a2</Hash>
</Codenesium>*/