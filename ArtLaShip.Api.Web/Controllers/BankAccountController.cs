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
	[Route("api/bankAccounts")]
	[ApiController]
	[ApiVersion("1.0")]

	public class BankAccountController : AbstractBankAccountController
	{
		public BankAccountController(
			ApiSettings settings,
			ILogger<BankAccountController> logger,
			ITransactionCoordinator transactionCoordinator,
			IBankAccountService bankAccountService,
			IApiBankAccountServerModelMapper bankAccountModelMapper
			)
			: base(settings,
			       logger,
			       transactionCoordinator,
			       bankAccountService,
			       bankAccountModelMapper)
		{
			this.BulkInsertLimit = 250;
			this.MaxLimit = 1000;
			this.DefaultLimit = 250;
		}
	}
}

/*<Codenesium>
    <Hash>ac594fcd484fd7e449db624336a28481</Hash>
</Codenesium>*/