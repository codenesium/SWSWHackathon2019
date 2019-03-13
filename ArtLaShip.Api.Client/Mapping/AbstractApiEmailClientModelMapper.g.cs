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
			                       request.Email1);
			return response;
		}

		public virtual ApiEmailClientRequestModel MapClientResponseToRequest(
			ApiEmailClientResponseModel response)
		{
			var request = new ApiEmailClientRequestModel();
			request.SetProperties(
				response.ArtistId,
				response.DateCreated,
				response.Email1);
			return request;
		}
	}
}

/*<Codenesium>
    <Hash>b3540e1270187d5719cdf361db4a6075</Hash>
</Codenesium>*/