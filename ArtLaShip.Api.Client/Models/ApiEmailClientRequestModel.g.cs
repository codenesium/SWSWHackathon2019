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
			DateTime dateCreated,
			string email1)
		{
			this.ArtistId = artistId;
			this.DateCreated = dateCreated;
			this.Email1 = email1;
		}

		[JsonProperty]
		public int ArtistId { get; private set; }

		[JsonProperty]
		public DateTime DateCreated { get; private set; } = SqlDateTime.MinValue.Value;

		[JsonProperty]
		public string Email1 { get; private set; } = default(string);
	}
}

/*<Codenesium>
    <Hash>7a1d5074270b830f31c8f188df53abf1</Hash>
</Codenesium>*/