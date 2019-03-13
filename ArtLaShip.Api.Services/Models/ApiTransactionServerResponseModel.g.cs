using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace ArtLaShipNS.Api.Services
{
	public partial class ApiTransactionServerResponseModel : AbstractApiServerResponseModel
	{
		public virtual void SetProperties(
			int id,
			decimal amount,
			int artistId,
			DateTime dateCreated,
			string stripeTransactionId)
		{
			this.Id = id;
			this.Amount = amount;
			this.ArtistId = artistId;
			this.DateCreated = dateCreated;
			this.StripeTransactionId = stripeTransactionId;
		}

		[JsonProperty]
		public decimal Amount { get; private set; }

		[JsonProperty]
		public int ArtistId { get; private set; }

		[JsonProperty]
		public string ArtistIdEntity { get; private set; } = RouteConstants.Artists;

		[JsonProperty]
		public ApiArtistServerResponseModel ArtistIdNavigation { get; private set; }

		public void SetArtistIdNavigation(ApiArtistServerResponseModel value)
		{
			this.ArtistIdNavigation = value;
		}

		[JsonProperty]
		public DateTime DateCreated { get; private set; }

		[JsonProperty]
		public int Id { get; private set; }

		[JsonProperty]
		public string StripeTransactionId { get; private set; }
	}
}

/*<Codenesium>
    <Hash>b7a6e06bb38d4f6f88531a7525364a3d</Hash>
</Codenesium>*/