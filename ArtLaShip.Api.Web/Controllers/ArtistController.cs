using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.Services;
using Codenesium.Foundation.CommonMVC;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Web
{
	[Route("api/artists")]
	[ApiController]
	[ApiVersion("1.0")]

	public class ArtistController : AbstractArtistController
	{
		public ArtistController(
			ApiSettings settings,
			ILogger<ArtistController> logger,
			ITransactionCoordinator transactionCoordinator,
			IArtistService artistService,
			IApiArtistServerModelMapper artistModelMapper
			)
			: base(settings,
			       logger,
			       transactionCoordinator,
			       artistService,
			       artistModelMapper)
		{
			this.BulkInsertLimit = 250;
			this.MaxLimit = 1000;
			this.DefaultLimit = 250;
		}
	}
}

/*<Codenesium>
    <Hash>42f925dde2bd3c4e6294a87de0d56892</Hash>
</Codenesium>*/