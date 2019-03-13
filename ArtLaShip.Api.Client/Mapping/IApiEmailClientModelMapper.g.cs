using ArtLaShipNS.Api.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Client
{
	public partial interface IApiEmailModelMapper
	{
		ApiEmailClientResponseModel MapClientRequestToResponse(
			int id,
			ApiEmailClientRequestModel request);

		ApiEmailClientRequestModel MapClientResponseToRequest(
			ApiEmailClientResponseModel response);
	}
}

/*<Codenesium>
    <Hash>dead1c7a5e35fb40538b2c2760c591be</Hash>
</Codenesium>*/