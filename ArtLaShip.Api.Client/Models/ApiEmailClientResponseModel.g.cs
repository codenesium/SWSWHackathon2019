using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ArtLaShipNS.Api.Client
{
	public partial class ApiEmailClientResponseModel : AbstractApiClientResponseModel
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

			this.ArtistIdEntity = nameof(ApiResponse.Artists);
		}

		[JsonProperty]
		public ApiArtistClientResponseModel ArtistIdNavigation { get; private set; }

		public void SetArtistIdNavigation(ApiArtistClientResponseModel value)
		{
			this.ArtistIdNavigation = value;
		}

		[JsonProperty]
		public int ArtistId { get; private set; }

		[JsonProperty]
		public string ArtistIdEntity { get; set; }

		[JsonProperty]
		public DateTime? DateCreated { get; private set; }

		[JsonProperty]
		public string EmailValue { get; private set; }

		[JsonProperty]
		public int Id { get; private set; }
	}
}

/*<Codenesium>
    <Hash>c7165fd9d5c3a7ca873d5ad71e2b60f4</Hash>
</Codenesium>*/