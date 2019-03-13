using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using System;
using System.Collections.Generic;

namespace ArtLaShipNS.Api.Services
{
	public abstract class AbstractDALTransactionMapper
	{
		public virtual Transaction MapModelToEntity(
			int id,
			ApiTransactionServerRequestModel model
			)
		{
			Transaction item = new Transaction();
			item.SetProperties(
				id,
				model.Amount,
				model.ArtistId,
				model.DateCreated,
				model.StripeTransactionId);
			return item;
		}

		public virtual ApiTransactionServerResponseModel MapEntityToModel(
			Transaction item)
		{
			var model = new ApiTransactionServerResponseModel();

			model.SetProperties(item.Id,
			                    item.Amount,
			                    item.ArtistId,
			                    item.DateCreated,
			                    item.StripeTransactionId);
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

		public virtual List<ApiTransactionServerResponseModel> MapEntityToModel(
			List<Transaction> items)
		{
			List<ApiTransactionServerResponseModel> response = new List<ApiTransactionServerResponseModel>();

			items.ForEach(x =>
			{
				response.Add(this.MapEntityToModel(x));
			});

			return response;
		}
	}
}

/*<Codenesium>
    <Hash>62559aadb87c810d5062e9aa926f0161</Hash>
</Codenesium>*/