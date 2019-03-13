using Codenesium.DataConversionExtensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq.Expressions;

namespace ArtLaShipNS.Api.Services
{
	public partial class ApiEmailServerRequestModel : AbstractApiServerRequestModel
	{
		public ApiEmailServerRequestModel()
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

		[Required]
		[JsonProperty]
		public int ArtistId { get; private set; }

		[Required]
		[JsonProperty]
		public DateTime DateCreated { get; private set; } = SqlDateTime.MinValue.Value;

		[Required]
		[JsonProperty]
		public string Email1 { get; private set; } = default(string);
	}
}

/*<Codenesium>
    <Hash>93d5585392ecca90929d6ab95b43dc36</Hash>
</Codenesium>*/