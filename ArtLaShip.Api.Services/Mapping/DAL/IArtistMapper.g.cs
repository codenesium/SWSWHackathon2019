using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using System;
using System.Collections.Generic;

namespace ArtLaShipNS.Api.Services
{
	public partial interface IDALArtistMapper
	{
		Artist MapModelToEntity(
			int id,
			ApiArtistServerRequestModel model);

		ApiArtistServerResponseModel MapEntityToModel(
			Artist item);

		List<ApiArtistServerResponseModel> MapEntityToModel(
			List<Artist> items);
	}
}

/*<Codenesium>
    <Hash>967ca309949144d112a45cf05bda4680</Hash>
</Codenesium>*/