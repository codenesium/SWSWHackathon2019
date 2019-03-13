using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public partial interface ITransactionService
	{
		Task<CreateResponse<ApiTransactionServerResponseModel>> Create(
			ApiTransactionServerRequestModel model);

		Task<UpdateResponse<ApiTransactionServerResponseModel>> Update(int id,
		                                                                ApiTransactionServerRequestModel model);

		Task<ActionResponse> Delete(int id);

		Task<ApiTransactionServerResponseModel> Get(int id);

		Task<List<ApiTransactionServerResponseModel>> All(int limit = int.MaxValue, int offset = 0, string query = "");

		Task<List<ApiTransactionServerResponseModel>> ByArtistId(int artistId, int limit = int.MaxValue, int offset = 0);
	}
}

/*<Codenesium>
    <Hash>893290354a90c143cc548ccba3c49be7</Hash>
</Codenesium>*/