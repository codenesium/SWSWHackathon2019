using Codenesium.DataConversionExtensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ArtLaShipNS.Api.DataAccess
{
	[Table("Email", Schema="dbo")]
	public partial class Email : AbstractEntity
	{
		public Email()
		{
		}

		public virtual void SetProperties(
			int id,
			int artistId,
			DateTime? dateCreated,
			string emailValue)
		{
			this.Id = id;
			this.ArtistId = artistId;
			this.DateCreated = dateCreated;
			this.EmailValue = emailValue;
		}

		[Column("artistId")]
		public virtual int ArtistId { get; private set; }

		[Column("dateCreated")]
		public virtual DateTime? DateCreated { get; private set; }

		[MaxLength(128)]
		[Column("email")]
		public virtual string EmailValue { get; private set; }

		[Key]
		[Column("id")]
		public virtual int Id { get; private set; }

		[ForeignKey("ArtistId")]
		public virtual Artist ArtistIdNavigation { get; private set; }

		public void SetArtistIdNavigation(Artist item)
		{
			this.ArtistIdNavigation = item;
		}
	}
}

/*<Codenesium>
    <Hash>7c9eee31a4a59425f96c9e6bb91e9890</Hash>
</Codenesium>*/