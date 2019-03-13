using Codenesium.DataConversionExtensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq.Expressions;

namespace ArtLaShipNS.Api.Services
{
	public partial class ApiArtistServerRequestModel : AbstractApiServerRequestModel
	{
		public ApiArtistServerRequestModel()
			: base()
		{
		}

		public virtual void SetProperties(
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

		[JsonProperty]
		public string AspNetUserId { get; private set; } = "unset";

		[JsonProperty]
		public string Bio { get; private set; } = default(string);

		[JsonProperty]
		public Guid ExternalId { get; private set; } = Guid.NewGuid();

		[JsonProperty]
		public string Facebook { get; private set; } = default(string);

		[Required]
		[JsonProperty]
		public string Name { get; private set; } = default(string);

		[JsonProperty]
		public string SoundCloud { get; private set; } = default(string);

		[JsonProperty]
		public string Twitter { get; private set; } = default(string);

		[Required]
		[JsonProperty]
		public string Venmo { get; private set; } = default(string);

		[JsonProperty]
		public string Website { get; private set; } = default(string);
	}
}

/*<Codenesium>
    <Hash>37c7b87be0c4b8f86a9352ac02ef97bd</Hash>
</Codenesium>*/