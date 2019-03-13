using Codenesium.DataConversionExtensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ArtLaShipNS.Api.DataAccess
{
	[Table("BankAccount", Schema="dbo")]
	public partial class BankAccount : AbstractEntity
	{
		public BankAccount()
		{
		}

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

		[MaxLength(24)]
		[Column("accountNumber")]
		public virtual string AccountNumber { get; private set; }

		[Column("artistId")]
		public virtual int ArtistId { get; private set; }

		[Key]
		[Column("id")]
		public virtual int Id { get; private set; }

		[MaxLength(24)]
		[Column("routingNumber")]
		public virtual string RoutingNumber { get; private set; }

		[ForeignKey("ArtistId")]
		public virtual Artist ArtistIdNavigation { get; private set; }

		public void SetArtistIdNavigation(Artist item)
		{
			this.ArtistIdNavigation = item;
		}
	}
}

/*<Codenesium>
    <Hash>c3094826fb189d925ee200a05fab9307</Hash>
</Codenesium>*/