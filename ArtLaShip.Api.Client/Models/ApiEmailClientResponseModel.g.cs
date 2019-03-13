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
			DateTime dateCreated,
			string email1)
		{
			this.Id = id;
			this.ArtistId = artistId;
			this.DateCreated = dateCreated;
			this.Email1 = email1;

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
		public DateTime DateCreated { get; private set; }

		[JsonProperty]
		public string Email1 { get; private set; }

		[JsonProperty]
		public int Id { get; private set; }
	}
}

/*<Codenesium>
    <Hash>b93004714cd6592d03e8f63b4bf1def3</Hash>
</Codenesium>*/