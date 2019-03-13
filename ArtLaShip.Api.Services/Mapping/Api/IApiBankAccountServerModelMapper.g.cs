using ArtLaShipNS.Api.Client;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public partial interface IApiBankAccountServerModelMapper
	{
		ApiBankAccountServerResponseModel MapServerRequestToResponse(
			int id,
			ApiBankAccountServerRequestModel request);

		ApiBankAccountServerRequestModel MapServerResponseToRequest(
			ApiBankAccountServerResponseModel response);

		ApiBankAccountClientRequestModel MapServerResponseToClientRequest(
			ApiBankAccountServerResponseModel response);

		JsonPatchDocument<ApiBankAccountServerRequestModel> CreatePatch(ApiBankAccountServerRequestModel model);
	}
}

/*<Codenesium>
    <Hash>6c2a7342473d6ba8c246cc8a44b81f7f</Hash>
</Codenesium>*/