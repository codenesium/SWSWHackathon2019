using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using System;
using System.Collections.Generic;

namespace ArtLaShipNS.Api.Services
{
	public partial interface IDALBankAccountMapper
	{
		BankAccount MapModelToEntity(
			int id,
			ApiBankAccountServerRequestModel model);

		ApiBankAccountServerResponseModel MapEntityToModel(
			BankAccount item);

		List<ApiBankAccountServerResponseModel> MapEntityToModel(
			List<BankAccount> items);
	}
}

/*<Codenesium>
    <Hash>c989abea2e9c0a1f6c1ac097307bcd32</Hash>
</Codenesium>*/