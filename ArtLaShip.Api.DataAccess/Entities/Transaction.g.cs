using Codenesium.DataConversionExtensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ArtLaShipNS.Api.DataAccess
{
	[Table("Transaction", Schema="dbo")]
	public partial class Transaction : AbstractEntity
	{
		public Transaction()
		{
		}

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

		[Column("amount")]
		public virtual decimal Amount { get; private set; }

		[Column("artistId")]
		public virtual int ArtistId { get; private set; }

		[Column("dateCreated")]
		public virtual DateTime DateCreated { get; private set; }

		[Key]
		[Column("id")]
		public virtual int Id { get; private set; }

		[MaxLength(128)]
		[Column("stripeTransactionId")]
		public virtual string StripeTransactionId { get; private set; }

		[ForeignKey("ArtistId")]
		public virtual Artist ArtistIdNavigation { get; private set; }

		public void SetArtistIdNavigation(Artist item)
		{
			this.ArtistIdNavigation = item;
		}
	}
}

/*<Codenesium>
    <Hash>17af6a6eddf7ee041386c199627ac926</Hash>
</Codenesium>*/