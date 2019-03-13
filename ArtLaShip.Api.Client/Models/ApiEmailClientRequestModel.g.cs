using Codenesium.DataConversionExtensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq.Expressions;

namespace ArtLaShipNS.Api.Client
{
	public partial class ApiEmailClientRequestModel : AbstractApiClientRequestModel
	{
		public ApiEmailClientRequestModel()
			: base()
		{
		}

		public virtual void SetProperties(
			int artistId,
			DateTime? dateCreated,
			string emailValue)
		{
			this.ArtistId = artistId;
			this.DateCreated = dateCreated;
			this.EmailValue = emailValue;
		}

		[JsonProperty]
		public int ArtistId { get; private set; }

		[JsonProperty]
		public DateTime? DateCreated { get; private set; } = DateTime.Now;

		[JsonProperty]
		public string EmailValue { get; private set; } = default(string);
	}
}

/*<Codenesium>
    <Hash>f9f8dd0bc77df605a201b7f7caadd96e</Hash>
</Codenesium>*/