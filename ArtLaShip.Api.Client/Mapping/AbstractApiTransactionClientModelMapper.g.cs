using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Client
{
	public abstract class AbstractApiTransactionModelMapper
	{
		public virtual ApiTransactionClientResponseModel MapClientRequestToResponse(
			int id,
			ApiTransactionClientRequestModel request)
		{
			var response = new ApiTransactionClientResponseModel();
			response.SetProperties(id,
			                       request.Amount,
			                       request.ArtistId,
			                       request.DateCreated,
			                       request.StripeTransactionId);
			return response;
		}

		public virtual ApiTransactionClientRequestModel MapClientResponseToRequest(
			ApiTransactionClientResponseModel response)
		{
			var request = new ApiTransactionClientRequestModel();
			request.SetProperties(
				response.Amount,
				response.ArtistId,
				response.DateCreated,
				response.StripeTransactionId);
			return request;
		}
	}
}

/*<Codenesium>
    <Hash>875024822318cc74c9925e4d89a58993</Hash>
</Codenesium>*/