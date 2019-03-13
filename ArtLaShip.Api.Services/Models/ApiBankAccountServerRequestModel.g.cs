using Codenesium.DataConversionExtensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq.Expressions;

namespace ArtLaShipNS.Api.Services
{
	public partial class ApiBankAccountServerRequestModel : AbstractApiServerRequestModel
	{
		public ApiBankAccountServerRequestModel()
			: base()
		{
		}

		public virtual void SetProperties(
			string accountNumber,
			int artistId,
			string routingNumber)
		{
			this.AccountNumber = accountNumber;
			this.ArtistId = artistId;
			this.RoutingNumber = routingNumber;
		}

		[Required]
		[JsonProperty]
		public string AccountNumber { get; private set; } = default(string);

		[Required]
		[JsonProperty]
		public int ArtistId { get; private set; }

		[Required]
		[JsonProperty]
		public string RoutingNumber { get; private set; } = default(string);
	}
}

/*<Codenesium>
    <Hash>a6579e99f3d3e0dc7c1c79208d2249df</Hash>
</Codenesium>*/