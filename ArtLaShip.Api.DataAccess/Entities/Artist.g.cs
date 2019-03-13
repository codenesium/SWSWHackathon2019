using Codenesium.DataConversionExtensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ArtLaShipNS.Api.DataAccess
{
	[Table("Artist", Schema="dbo")]
	public partial class Artist : AbstractEntity
	{
		public Artist()
		{
		}

		public virtual void SetProperties(
			int id,
			string aspNetUserId,
			string bio,
			Guid externalId,
			string facebook,
			string name,
			string soundCloud,
			string twitter,
			string venmo,
			string website)
		{
			this.Id = id;
			this.AspNetUserId = aspNetUserId;
			this.Bio = bio;
			this.ExternalId = externalId;
			this.Facebook = facebook;
			this.Name = name;
			this.SoundCloud = soundCloud;
			this.Twitter = twitter;
			this.Venmo = venmo;
			this.Website = website;
		}

		[MaxLength(450)]
		[Column("aspNetUserId")]
		public virtual string AspNetUserId { get; private set; }

		[MaxLength(8000)]
		[Column("bio")]
		public virtual string Bio { get; private set; }

		[Column("externalId")]
		public virtual Guid ExternalId { get; private set; }

		[MaxLength(128)]
		[Column("facebook")]
		public virtual string Facebook { get; private set; }

		[Key]
		[Column("id")]
		public virtual int Id { get; private set; }

		[MaxLength(128)]
		[Column("name")]
		public virtual string Name { get; private set; }

		[MaxLength(128)]
		[Column("soundCloud")]
		public virtual string SoundCloud { get; private set; }

		[MaxLength(128)]
		[Column("twitter")]
		public virtual string Twitter { get; private set; }

		[MaxLength(128)]
		[Column("venmo")]
		public virtual string Venmo { get; private set; }

		[MaxLength(128)]
		[Column("website")]
		public virtual string Website { get; private set; }
	}
}

/*<Codenesium>
    <Hash>aa3f42f7f80845430f0801ea196c1851</Hash>
</Codenesium>*/