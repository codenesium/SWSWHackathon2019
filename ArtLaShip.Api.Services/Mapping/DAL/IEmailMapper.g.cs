using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using System;
using System.Collections.Generic;

namespace ArtLaShipNS.Api.Services
{
	public partial interface IDALEmailMapper
	{
		Email MapModelToEntity(
			int id,
			ApiEmailServerRequestModel model);

		ApiEmailServerResponseModel MapEntityToModel(
			Email item);

		List<ApiEmailServerResponseModel> MapEntityToModel(
			List<Email> items);
	}
}

/*<Codenesium>
    <Hash>dcfcfcc562f35897db96c09b02596b3a</Hash>
</Codenesium>*/