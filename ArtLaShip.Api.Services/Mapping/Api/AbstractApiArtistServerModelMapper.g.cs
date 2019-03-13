using ArtLaShipNS.Api.Client;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public abstract class AbstractApiArtistServerModelMapper
	{
		public virtual ApiArtistServerResponseModel MapServerRequestToResponse(
			int id,
			ApiArtistServerRequestModel request)
		{
			var response = new ApiArtistServerResponseModel();
			response.SetProperties(id,
			                       request.AspNetUserId,
			                       request.Bio,
			                       request.ExternalId,
			                       request.Facebook,
			                       request.Name,
			                       request.SoundCloud,
			                       request.Twitter,
			                       request.Website);
			return response;
		}

		public virtual ApiArtistServerRequestModel MapServerResponseToRequest(
			ApiArtistServerResponseModel response)
		{
			var request = new ApiArtistServerRequestModel();
			request.SetProperties(
				response.AspNetUserId,
				response.Bio,
				response.ExternalId,
				response.Facebook,
				response.Name,
				response.SoundCloud,
				response.Twitter,
				response.Website);
			return request;
		}

		public virtual ApiArtistClientRequestModel MapServerResponseToClientRequest(
			ApiArtistServerResponseModel response)
		{
			var request = new ApiArtistClientRequestModel();
			request.SetProperties(
				response.AspNetUserId,
				response.Bio,
				response.ExternalId,
				response.Facebook,
				response.Name,
				response.SoundCloud,
				response.Twitter,
				response.Website);
			return request;
		}

		public JsonPatchDocument<ApiArtistServerRequestModel> CreatePatch(ApiArtistServerRequestModel model)
		{
			var patch = new JsonPatchDocument<ApiArtistServerRequestModel>();
			patch.Replace(x => x.AspNetUserId, model.AspNetUserId);
			patch.Replace(x => x.Bio, model.Bio);
			patch.Replace(x => x.ExternalId, model.ExternalId);
			patch.Replace(x => x.Facebook, model.Facebook);
			patch.Replace(x => x.Name, model.Name);
			patch.Replace(x => x.SoundCloud, model.SoundCloud);
			patch.Replace(x => x.Twitter, model.Twitter);
			patch.Replace(x => x.Website, model.Website);
			return patch;
		}
	}
}

/*<Codenesium>
    <Hash>c32495c974021f463f835bc600c729de</Hash>
</Codenesium>*/