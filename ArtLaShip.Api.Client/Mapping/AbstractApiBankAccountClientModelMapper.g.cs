using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Client
{
	public abstract class AbstractApiBankAccountModelMapper
	{
		public virtual ApiBankAccountClientResponseModel MapClientRequestToResponse(
			int id,
			ApiBankAccountClientRequestModel request)
		{
			var response = new ApiBankAccountClientResponseModel();
			response.SetProperties(id,
			                       request.AccountNumber,
			                       request.ArtistId,
			                       request.RoutingNumber);
			return response;
		}

		public virtual ApiBankAccountClientRequestModel MapClientResponseToRequest(
			ApiBankAccountClientResponseModel response)
		{
			var request = new ApiBankAccountClientRequestModel();
			request.SetProperties(
				response.AccountNumber,
				response.ArtistId,
				response.RoutingNumber);
			return request;
		}
	}
}

/*<Codenesium>
    <Hash>3984ff0260d0b7220f18e64146b476b7</Hash>
</Codenesium>*/