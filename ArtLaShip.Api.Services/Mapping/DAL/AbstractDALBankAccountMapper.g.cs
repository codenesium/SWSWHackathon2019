using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using System;
using System.Collections.Generic;

namespace ArtLaShipNS.Api.Services
{
	public abstract class AbstractDALBankAccountMapper
	{
		public virtual BankAccount MapModelToEntity(
			int id,
			ApiBankAccountServerRequestModel model
			)
		{
			BankAccount item = new BankAccount();
			item.SetProperties(
				id,
				model.AccountNumber,
				model.ArtistId,
				model.RoutingNumber);
			return item;
		}

		public virtual ApiBankAccountServerResponseModel MapEntityToModel(
			BankAccount item)
		{
			var model = new ApiBankAccountServerResponseModel();

			model.SetProperties(item.Id,
			                    item.AccountNumber,
			                    item.ArtistId,
			                    item.RoutingNumber);
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

		public virtual List<ApiBankAccountServerResponseModel> MapEntityToModel(
			List<BankAccount> items)
		{
			List<ApiBankAccountServerResponseModel> response = new List<ApiBankAccountServerResponseModel>();

			items.ForEach(x =>
			{
				response.Add(this.MapEntityToModel(x));
			});

			return response;
		}
	}
}

/*<Codenesium>
    <Hash>e8e3e3551f0399ace0f5fc917eb380f3</Hash>
</Codenesium>*/