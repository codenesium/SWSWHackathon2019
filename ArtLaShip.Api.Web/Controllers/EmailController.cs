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
	[Route("api/emails")]
	[ApiController]
	[ApiVersion("1.0")]

	public class EmailController : AbstractEmailController
	{
		public EmailController(
			ApiSettings settings,
			ILogger<EmailController> logger,
			ITransactionCoordinator transactionCoordinator,
			IEmailService emailService,
			IApiEmailServerModelMapper emailModelMapper
			)
			: base(settings,
			       logger,
			       transactionCoordinator,
			       emailService,
			       emailModelMapper)
		{
			this.BulkInsertLimit = 250;
			this.MaxLimit = 1000;
			this.DefaultLimit = 250;
		}
	}
}

/*<Codenesium>
    <Hash>c38f25ca3725cb41cc55e15dfccabc0c</Hash>
</Codenesium>*/