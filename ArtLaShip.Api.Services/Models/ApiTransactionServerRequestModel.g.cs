using Codenesium.DataConversionExtensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq.Expressions;

namespace ArtLaShipNS.Api.Services
{
	public partial class ApiTransactionServerRequestModel : AbstractApiServerRequestModel
	{
		public ApiTransactionServerRequestModel()
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

		[Required]
		[JsonProperty]
		public decimal Amount { get; private set; } = default(decimal);

		[Required]
		[JsonProperty]
		public int ArtistId { get; private set; }

		[Required]
		[JsonProperty]
		public DateTime DateCreated { get; private set; } = SqlDateTime.MinValue.Value;

		[Required]
		[JsonProperty]
		public string StripeTransactionId { get; private set; } = default(string);
	}
}

/*<Codenesium>
    <Hash>85067a75f3e8e7163b0b30c9899631a1</Hash>
</Codenesium>*/