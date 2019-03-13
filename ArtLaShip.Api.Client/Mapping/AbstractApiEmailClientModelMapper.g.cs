using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Client
{
	public abstract class AbstractApiEmailModelMapper
	{
		public virtual ApiEmailClientResponseModel MapClientRequestToResponse(
			int id,
			ApiEmailClientRequestModel request)
		{
			var response = new ApiEmailClientResponseModel();
			response.SetProperties(id,
			                       request.ArtistId,
			                       request.DateCreated,
			                       request.EmailValue);
			return response;
		}

		public virtual ApiEmailClientRequestModel MapClientResponseToRequest(
			ApiEmailClientResponseModel response)
		{
			var request = new ApiEmailClientRequestModel();
			request.SetProperties(
				response.ArtistId,
				response.DateCreated,
				response.EmailValue);
			return request;
		}
	}
}

/*<Codenesium>
    <Hash>b8dd228cd7f90e2f77915cd6f63eb279</Hash>
</Codenesium>*/