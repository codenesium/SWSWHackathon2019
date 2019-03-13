using ArtLaShipNS.Api.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Client
{
	public partial interface IApiBankAccountModelMapper
	{
		ApiBankAccountClientResponseModel MapClientRequestToResponse(
			int id,
			ApiBankAccountClientRequestModel request);

		ApiBankAccountClientRequestModel MapClientResponseToRequest(
			ApiBankAccountClientResponseModel response);
	}
}

/*<Codenesium>
    <Hash>f099d12372e9df515896e679cc60762d</Hash>
</Codenesium>*/