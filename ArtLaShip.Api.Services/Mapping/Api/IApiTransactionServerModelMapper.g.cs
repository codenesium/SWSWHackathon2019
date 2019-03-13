using ArtLaShipNS.Api.Client;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public partial interface IApiTransactionServerModelMapper
	{
		ApiTransactionServerResponseModel MapServerRequestToResponse(
			int id,
			ApiTransactionServerRequestModel request);

		ApiTransactionServerRequestModel MapServerResponseToRequest(
			ApiTransactionServerResponseModel response);

		ApiTransactionClientRequestModel MapServerResponseToClientRequest(
			ApiTransactionServerResponseModel response);

		JsonPatchDocument<ApiTransactionServerRequestModel> CreatePatch(ApiTransactionServerRequestModel model);
	}
}

/*<Codenesium>
    <Hash>180f122fd49928f1e366576763cc1b02</Hash>
</Codenesium>*/