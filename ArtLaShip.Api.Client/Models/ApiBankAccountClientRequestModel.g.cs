using Codenesium.DataConversionExtensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq.Expressions;

namespace ArtLaShipNS.Api.Client
{
	public partial class ApiBankAccountClientRequestModel : AbstractApiClientRequestModel
	{
		public ApiBankAccountClientRequestModel()
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

		[JsonProperty]
		public string AccountNumber { get; private set; } = default(string);

		[JsonProperty]
		public int ArtistId { get; private set; }

		[JsonProperty]
		public string RoutingNumber { get; private set; } = default(string);
	}
}

/*<Codenesium>
    <Hash>783a6729ea001ec3c052691c28bd326d</Hash>
</Codenesium>*/