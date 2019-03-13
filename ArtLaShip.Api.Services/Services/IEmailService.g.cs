using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public partial interface IEmailService
	{
		Task<CreateResponse<ApiEmailServerResponseModel>> Create(
			ApiEmailServerRequestModel model);

		Task<UpdateResponse<ApiEmailServerResponseModel>> Update(int id,
		                                                          ApiEmailServerRequestModel model);

		Task<ActionResponse> Delete(int id);

		Task<ApiEmailServerResponseModel> Get(int id);

		Task<List<ApiEmailServerResponseModel>> All(int limit = int.MaxValue, int offset = 0, string query = "");

		Task<List<ApiEmailServerResponseModel>> ByArtistId(int artistId, int limit = int.MaxValue, int offset = 0);
	}
}

/*<Codenesium>
    <Hash>3812e9e1eff9bec941e33402068f84ab</Hash>
</Codenesium>*/