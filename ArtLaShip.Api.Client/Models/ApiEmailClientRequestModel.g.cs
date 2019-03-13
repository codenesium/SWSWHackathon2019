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
		public DateTime? DateCreated { get; private set; } = null;

		[JsonProperty]
		public string EmailValue { get; private set; } = default(string);
	}
}

/*<Codenesium>
    <Hash>b45cb72491230a1e9da05890f6be7931</Hash>
</Codenesium>*/