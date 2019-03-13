using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.Services;
using ArtLaShipNS.Api.Web.Models;
using Codenesium.Foundation.CommonMVC;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Web
{
	[Route("api/payments")]
	[ApiController]
	[ApiVersion("1.0")]

	public class PaymentController : AbstractApiController
	{
		public PaymentController(
			ApiSettings settings,
			ILogger<PaymentController> logger,
			ITransactionCoordinator transactionCoordinator
			)
			: base(settings,
			       logger,
			       transactionCoordinator)
		{
		}

		[HttpPost]
		[Route("process")]
		[UnitOfWork]
		[ProducesResponseType(typeof(UpdateResponse<ApiArtistServerResponseModel>), 200)]
		[ProducesResponseType(typeof(void), 404)]
		[ProducesResponseType(typeof(ActionResponse), 422)]

		public virtual async Task<IActionResult> Process(PaymentModel model)
		{

			var customers = new CustomerService(this.Settings.StripeSecretKey);
			var charges = new ChargeService(this.Settings.StripeSecretKey);
			var cards = new CardService(this.Settings.StripeSecretKey);

			var customer = customers.Create(new CustomerCreateOptions
			{
				Email = model.Email,
				SourceToken = model.Id
			});

			//var card = cards.Create(customer.Id, new CardCreateOptions()
			//{
			//	SourceToken = model.StripeToken 
			//});


			var charge = charges.Create(new ChargeCreateOptions
			{
				Amount = model.AmountInCents,
				Description = "Band tip",
				Currency = "usd",
				CustomerId = customer.Id
			});

			return this.Ok();

		}
	}
}

/*<Codenesium>
    <Hash>c38f25ca3725cb41cc55e15dfccabc0c</Hash>
</Codenesium>*/