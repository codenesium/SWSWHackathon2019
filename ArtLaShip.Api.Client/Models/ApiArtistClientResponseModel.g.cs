using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ArtLaShipNS.Api.Client
{
	public partial class ApiArtistClientResponseModel : AbstractApiClientResponseModel
	{
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

		[JsonProperty]
		public string AspNetUserId { get; private set; }

		[JsonProperty]
		public string Bio { get; private set; }

		[JsonProperty]
		public Guid ExternalId { get; private set; }

		[JsonProperty]
		public string Facebook { get; private set; }

		[JsonProperty]
		public int Id { get; private set; }

		[JsonProperty]
		public string Name { get; private set; }

		[JsonProperty]
		public string SoundCloud { get; private set; }

		[JsonProperty]
		public string Twitter { get; private set; }

		[JsonProperty]
		public string Venmo { get; private set; }

		[JsonProperty]
		public string Website { get; private set; }
	}
}

/*<Codenesium>
    <Hash>20facb81c766a4aaba50c248cdbd8d4e</Hash>
</Codenesium>*/