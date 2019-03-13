using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ArtLaShipNS.Api.Client
{
	public partial class ApiTransactionClientResponseModel : AbstractApiClientResponseModel
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

			this.ArtistIdEntity = nameof(ApiResponse.Artists);
		}

		[JsonProperty]
		public ApiArtistClientResponseModel ArtistIdNavigation { get; private set; }

		public void SetArtistIdNavigation(ApiArtistClientResponseModel value)
		{
			this.ArtistIdNavigation = value;
		}

		[JsonProperty]
		public decimal Amount { get; private set; }

		[JsonProperty]
		public int ArtistId { get; private set; }

		[JsonProperty]
		public string ArtistIdEntity { get; set; }

		[JsonProperty]
		public DateTime DateCreated { get; private set; }

		[JsonProperty]
		public int Id { get; private set; }

		[JsonProperty]
		public string StripeTransactionId { get; private set; }
	}
}

/*<Codenesium>
    <Hash>4bf6b3f667b79344ce5292f9371f59d0</Hash>
</Codenesium>*/