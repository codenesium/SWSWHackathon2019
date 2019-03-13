using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace ArtLaShipNS.Api.Services
{
	public partial class ApiBankAccountServerResponseModel : AbstractApiServerResponseModel
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
		}

		[JsonProperty]
		public string AccountNumber { get; private set; }

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
		public int Id { get; private set; }

		[JsonProperty]
		public string RoutingNumber { get; private set; }
	}
}

/*<Codenesium>
    <Hash>1d060bfc15facb1d1b064e65df6a06c9</Hash>
</Codenesium>*/