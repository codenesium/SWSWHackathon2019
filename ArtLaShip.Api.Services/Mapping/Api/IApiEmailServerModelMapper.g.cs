using ArtLaShipNS.Api.Client;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public partial interface IApiEmailServerModelMapper
	{
		ApiEmailServerResponseModel MapServerRequestToResponse(
			int id,
			ApiEmailServerRequestModel request);

		ApiEmailServerRequestModel MapServerResponseToRequest(
			ApiEmailServerResponseModel response);

		ApiEmailClientRequestModel MapServerResponseToClientRequest(
			ApiEmailServerResponseModel response);

		JsonPatchDocument<ApiEmailServerRequestModel> CreatePatch(ApiEmailServerRequestModel model);
	}
}

/*<Codenesium>
    <Hash>d4cbc3208c21075e5e6b067cfc909eba</Hash>
</Codenesium>*/