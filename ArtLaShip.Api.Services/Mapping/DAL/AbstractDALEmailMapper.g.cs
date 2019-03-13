using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using System;
using System.Collections.Generic;

namespace ArtLaShipNS.Api.Services
{
	public abstract class AbstractDALEmailMapper
	{
		public virtual Email MapModelToEntity(
			int id,
			ApiEmailServerRequestModel model
			)
		{
			Email item = new Email();
			item.SetProperties(
				id,
				model.ArtistId,
				model.DateCreated,
				model.Email1);
			return item;
		}

		public virtual ApiEmailServerResponseModel MapEntityToModel(
			Email item)
		{
			var model = new ApiEmailServerResponseModel();

			model.SetProperties(item.Id,
			                    item.ArtistId,
			                    item.DateCreated,
			                    item.Email1);
			if (item.ArtistIdNavigation != null)
			{
				var artistIdModel = new ApiArtistServerResponseModel();
				artistIdModel.SetProperties(
					item.ArtistIdNavigation.Id,
					item.ArtistIdNavigation.AspNetUserId,
					item.ArtistIdNavigation.Bio,
					item.ArtistIdNavigation.ExternalId,
					item.ArtistIdNavigation.Facebook,
					item.ArtistIdNavigation.Name,
					item.ArtistIdNavigation.SoundCloud,
					item.ArtistIdNavigation.Twitter,
					item.ArtistIdNavigation.Website);

				model.SetArtistIdNavigation(artistIdModel);
			}

			return model;
		}

		public virtual List<ApiEmailServerResponseModel> MapEntityToModel(
			List<Email> items)
		{
			List<ApiEmailServerResponseModel> response = new List<ApiEmailServerResponseModel>();

			items.ForEach(x =>
			{
				response.Add(this.MapEntityToModel(x));
			});

			return response;
		}
	}
}

/*<Codenesium>
    <Hash>ed778c822275153c53382aa670cca3b1</Hash>
</Codenesium>*/