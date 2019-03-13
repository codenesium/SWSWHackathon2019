using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Web.Models
{
    public class PaymentModel
    {
		public string StripeToken { get; set; }

		public string Email { get; set; }

		public long AmountInCents { get; set; }
	}
}
