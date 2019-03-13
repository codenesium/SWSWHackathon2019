using Codenesium.DataConversionExtensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq.Expressions;

namespace ArtLaShipNS.Api.Client
{
	public partial class ApiTransactionClientRequestModel : AbstractApiClientRequestModel
	{
		public ApiTransactionClientRequestModel()
			: base()
		{
		}

		public virtual void SetProperties(
			decimal amount,
			int artistId,
			DateTime dateCreated,
			string stripeTransactionId)
		{
			this.Amount = amount;
			this.ArtistId = artistId;
			this.DateCreated = dateCreated;
			this.StripeTransactionId = stripeTransactionId;
		}

		[JsonProperty]
		public decimal Amount { get; private set; } = default(decimal);

		[JsonProperty]
		public int ArtistId { get; private set; }

		[JsonProperty]
		public DateTime DateCreated { get; private set; } = SqlDateTime.MinValue.Value;

		[JsonProperty]
		public string StripeTransactionId { get; private set; } = default(string);
	}
}

/*<Codenesium>
    <Hash>d30db131e861ff70a9d224c1388fa0ee</Hash>
</Codenesium>*/