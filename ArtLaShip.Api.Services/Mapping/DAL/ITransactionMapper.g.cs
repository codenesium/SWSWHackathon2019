using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using System;
using System.Collections.Generic;

namespace ArtLaShipNS.Api.Services
{
	public partial interface IDALTransactionMapper
	{
		Transaction MapModelToEntity(
			int id,
			ApiTransactionServerRequestModel model);

		ApiTransactionServerResponseModel MapEntityToModel(
			Transaction item);

		List<ApiTransactionServerResponseModel> MapEntityToModel(
			List<Transaction> items);
	}
}

/*<Codenesium>
    <Hash>f8ad5325c5086495c497ce6eace88822</Hash>
</Codenesium>*/