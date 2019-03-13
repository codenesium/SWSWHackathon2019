using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public partial interface IArtistService
	{
		Task<CreateResponse<ApiArtistServerResponseModel>> Create(
			ApiArtistServerRequestModel model);

		Task<UpdateResponse<ApiArtistServerResponseModel>> Update(int id,
		                                                           ApiArtistServerRequestModel model);

		Task<ActionResponse> Delete(int id);

		Task<ApiArtistServerResponseModel> Get(int id);

		Task<List<ApiArtistServerResponseModel>> All(int limit = int.MaxValue, int offset = 0, string query = "");

		Task<List<ApiBankAccountServerResponseModel>> BankAccountsByArtistId(int artistId, int limit = int.MaxValue, int offset = 0);

		Task<List<ApiEmailServerResponseModel>> EmailsByArtistId(int artistId, int limit = int.MaxValue, int offset = 0);

		Task<List<ApiTransactionServerResponseModel>> TransactionsByArtistId(int artistId, int limit = int.MaxValue, int offset = 0);
	}
}

/*<Codenesium>
    <Hash>27817d0d00a02494c848583491ba33ca</Hash>
</Codenesium>*/