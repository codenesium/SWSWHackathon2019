using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public partial interface IBankAccountService
	{
		Task<CreateResponse<ApiBankAccountServerResponseModel>> Create(
			ApiBankAccountServerRequestModel model);

		Task<UpdateResponse<ApiBankAccountServerResponseModel>> Update(int id,
		                                                                ApiBankAccountServerRequestModel model);

		Task<ActionResponse> Delete(int id);

		Task<ApiBankAccountServerResponseModel> Get(int id);

		Task<List<ApiBankAccountServerResponseModel>> All(int limit = int.MaxValue, int offset = 0, string query = "");

		Task<List<ApiBankAccountServerResponseModel>> ByArtistId(int artistId, int limit = int.MaxValue, int offset = 0);
	}
}

/*<Codenesium>
    <Hash>276f6f5b0c233a4fca3a2f35078c64be</Hash>
</Codenesium>*/