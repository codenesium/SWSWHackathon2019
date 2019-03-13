using ArtLaShipNS.Api.Client;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public abstract class AbstractApiBankAccountServerModelMapper
	{
		public virtual ApiBankAccountServerResponseModel MapServerRequestToResponse(
			int id,
			ApiBankAccountServerRequestModel request)
		{
			var response = new ApiBankAccountServerResponseModel();
			response.SetProperties(id,
			                       request.AccountNumber,
			                       request.ArtistId,
			                       request.RoutingNumber);
			return response;
		}

		public virtual ApiBankAccountServerRequestModel MapServerResponseToRequest(
			ApiBankAccountServerResponseModel response)
		{
			var request = new ApiBankAccountServerRequestModel();
			request.SetProperties(
				response.AccountNumber,
				response.ArtistId,
				response.RoutingNumber);
			return request;
		}

		public virtual ApiBankAccountClientRequestModel MapServerResponseToClientRequest(
			ApiBankAccountServerResponseModel response)
		{
			var request = new ApiBankAccountClientRequestModel();
			request.SetProperties(
				response.AccountNumber,
				response.ArtistId,
				response.RoutingNumber);
			return request;
		}

		public JsonPatchDocument<ApiBankAccountServerRequestModel> CreatePatch(ApiBankAccountServerRequestModel model)
		{
			var patch = new JsonPatchDocument<ApiBankAccountServerRequestModel>();
			patch.Replace(x => x.AccountNumber, model.AccountNumber);
			patch.Replace(x => x.ArtistId, model.ArtistId);
			patch.Replace(x => x.RoutingNumber, model.RoutingNumber);
			return patch;
		}
	}
}

/*<Codenesium>
    <Hash>5d692b62d08c77d5909e24d71f7a5cf2</Hash>
</Codenesium>*/