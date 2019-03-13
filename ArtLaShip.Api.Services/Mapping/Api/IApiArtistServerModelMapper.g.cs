using ArtLaShipNS.Api.Client;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public partial interface IApiArtistServerModelMapper
	{
		ApiArtistServerResponseModel MapServerRequestToResponse(
			int id,
			ApiArtistServerRequestModel request);

		ApiArtistServerRequestModel MapServerResponseToRequest(
			ApiArtistServerResponseModel response);

		ApiArtistClientRequestModel MapServerResponseToClientRequest(
			ApiArtistServerResponseModel response);

		JsonPatchDocument<ApiArtistServerRequestModel> CreatePatch(ApiArtistServerRequestModel model);
	}
}

/*<Codenesium>
    <Hash>7e06651680642b626a31c92faf397e1f</Hash>
</Codenesium>*/