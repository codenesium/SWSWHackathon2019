using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ArtLaShipNS.Api.Client
{
	public partial class ApiBankAccountClientResponseModel : AbstractApiClientResponseModel
	{
		public virtual void SetProperties(
			int id,
			string accountNumber,
			int artistId,
			string routingNumber)
		{
			this.Id = id;
			this.AccountNumber = accountNumber;
			this.ArtistId = artistId;
			this.RoutingNumber = routingNumber;

			this.ArtistIdEntity = nameof(ApiResponse.Artists);
		}

		[JsonProperty]
		public ApiArtistClientResponseModel ArtistIdNavigation { get; private set; }

		public void SetArtistIdNavigation(ApiArtistClientResponseModel value)
		{
			this.ArtistIdNavigation = value;
		}

		[JsonProperty]
		public string AccountNumber { get; private set; }

		[JsonProperty]
		public int ArtistId { get; private set; }

		[JsonProperty]
		public string ArtistIdEntity { get; set; }

		[JsonProperty]
		public int Id { get; private set; }

		[JsonProperty]
		public string RoutingNumber { get; private set; }
	}
}

/*<Codenesium>
    <Hash>b57307acbb4a16fe9b77068b7c522834</Hash>
</Codenesium>*/