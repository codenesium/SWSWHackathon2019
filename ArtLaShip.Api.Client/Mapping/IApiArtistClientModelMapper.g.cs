using ArtLaShipNS.Api.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Client
{
	public partial interface IApiArtistModelMapper
	{
		ApiArtistClientResponseModel MapClientRequestToResponse(
			int id,
			ApiArtistClientRequestModel request);

		ApiArtistClientRequestModel MapClientResponseToRequest(
			ApiArtistClientResponseModel response);
	}
}

/*<Codenesium>
    <Hash>a67659fb38fbd5b52b989bd84fe63d5d</Hash>
</Codenesium>*/