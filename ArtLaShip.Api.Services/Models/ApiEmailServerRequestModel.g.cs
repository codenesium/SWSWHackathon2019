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
			DateTime? dateCreated,
			string emailValue)
		{
			this.ArtistId = artistId;
			this.DateCreated = dateCreated;
			this.EmailValue = emailValue;
		}

		[Required]
		[JsonProperty]
		public int ArtistId { get; private set; }

		[JsonProperty]
		public DateTime? DateCreated { get; private set; } = DateTime.Now;

		[Required]
		[JsonProperty]
		public string EmailValue { get; private set; } = default(string);
	}
}

/*<Codenesium>
    <Hash>a2d30d3e09dfb8bc042bd68cf6632241</Hash>
</Codenesium>*/