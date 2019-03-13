using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Client
{
	public abstract class AbstractApiArtistModelMapper
	{
		public virtual ApiArtistClientResponseModel MapClientRequestToResponse(
			int id,
			ApiArtistClientRequestModel request)
		{
			var response = new ApiArtistClientResponseModel();
			response.SetProperties(id,
			                       request.AspNetUserId,
			                       request.Bio,
			                       request.ExternalId,
			                       request.Facebook,
			                       request.Name,
			                       request.SoundCloud,
			                       request.Twitter,
			                       request.Venmo,
			                       request.Website);
			return response;
		}

		public virtual ApiArtistClientRequestModel MapClientResponseToRequest(
			ApiArtistClientResponseModel response)
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
				response.Venmo,
				response.Website);
			return request;
		}
	}
}

/*<Codenesium>
    <Hash>f4e46bfc88ff4e0124c5ad37dcd3c05d</Hash>
</Codenesium>*/