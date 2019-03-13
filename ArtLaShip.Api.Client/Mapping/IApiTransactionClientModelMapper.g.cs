using ArtLaShipNS.Api.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Client
{
	public partial interface IApiTransactionModelMapper
	{
		ApiTransactionClientResponseModel MapClientRequestToResponse(
			int id,
			ApiTransactionClientRequestModel request);

		ApiTransactionClientRequestModel MapClientResponseToRequest(
			ApiTransactionClientResponseModel response);
	}
}

/*<Codenesium>
    <Hash>dd80ac9ad2e7e07ce2506656ea22a4d0</Hash>
</Codenesium>*/