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
			DateTime dateCreated,
			string email1)
		{
			this.Id = id;
			this.ArtistId = artistId;
			this.DateCreated = dateCreated;
			this.Email1 = email1;
		}

		[Column("artistId")]
		public virtual int ArtistId { get; private set; }

		[Column("dateCreated")]
		public virtual DateTime DateCreated { get; private set; }

		[MaxLength(128)]
		[Column("email")]
		public virtual string Email1 { get; private set; }

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
    <Hash>638167deadf1421143b90a012ce1d425</Hash>
</Codenesium>*/