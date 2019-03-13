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
			DateTime dateCreated,
			string email1)
		{
			this.Id = id;
			this.ArtistId = artistId;
			this.DateCreated = dateCreated;
			this.Email1 = email1;
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

		[JsonProperty]
		public DateTime DateCreated { get; private set; }

		[JsonProperty]
		public string Email1 { get; private set; }

		[JsonProperty]
		public int Id { get; private set; }
	}
}

/*<Codenesium>
    <Hash>9ce2f8e4ba6d207079fb281143dd9844</Hash>
</Codenesium>*/