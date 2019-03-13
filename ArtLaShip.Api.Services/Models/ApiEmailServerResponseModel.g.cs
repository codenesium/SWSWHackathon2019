using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace ArtLaShipNS.Api.Services
{
	public partial class ApiEmailServerResponseModel : AbstractApiServerResponseModel
	{
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

		[JsonProperty]
		public int ArtistId { get; private set; }

		[JsonProperty]
		public string ArtistIdEntity { get; private set; } = RouteConstants.Artists;

		[JsonProperty]
		public ApiArtistServerResponseModel ArtistIdNavigation { get; private set; }

		public void SetArtistIdNavigation(ApiArtistServerResponseModel value)
		{
			this.ArtistIdNavigation = value;
		}

		[Required]
		[JsonProperty]
		public DateTime? DateCreated { get; private set; }

		[JsonProperty]
		public string EmailValue { get; private set; }

		[JsonProperty]
		public int Id { get; private set; }
	}
}

/*<Codenesium>
    <Hash>733ee84509174c82d670f8051a40b892</Hash>
</Codenesium>*/