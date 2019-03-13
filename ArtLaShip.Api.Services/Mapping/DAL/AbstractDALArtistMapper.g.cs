using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using System;
using System.Collections.Generic;

namespace ArtLaShipNS.Api.Services
{
	public abstract class AbstractDALArtistMapper
	{
		public virtual Artist MapModelToEntity(
			int id,
			ApiArtistServerRequestModel model
			)
		{
			Artist item = new Artist();
			item.SetProperties(
				id,
				model.AspNetUserId,
				model.Bio,
				model.ExternalId,
				model.Facebook,
				model.Name,
				model.SoundCloud,
				model.Twitter,
				model.Venmo,
				model.Website);
			return item;
		}

		public virtual ApiArtistServerResponseModel MapEntityToModel(
			Artist item)
		{
			var model = new ApiArtistServerResponseModel();

			model.SetProperties(item.Id,
			                    item.AspNetUserId,
			                    item.Bio,
			                    item.ExternalId,
			                    item.Facebook,
			                    item.Name,
			                    item.SoundCloud,
			                    item.Twitter,
			                    item.Venmo,
			                    item.Website);

			return model;
		}

		public virtual List<ApiArtistServerResponseModel> MapEntityToModel(
			List<Artist> items)
		{
			List<ApiArtistServerResponseModel> response = new List<ApiArtistServerResponseModel>();

			items.ForEach(x =>
			{
				response.Add(this.MapEntityToModel(x));
			});

			return response;
		}
	}
}

/*<Codenesium>
    <Hash>33e99099dbc0f2fb37d3ed34d66dd359</Hash>
</Codenesium>*/