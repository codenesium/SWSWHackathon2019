using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace ArtLaShipNS.Api.Client.Tests
{
	[Trait("Type", "Unit")]
	[Trait("Table", "Transaction")]
	[Trait("Area", "ApiModel")]
	public class TestApiTransactionModelMapper
	{
		[Fact]
		public void MapClientRequestToResponse()
		{
			var mapper = new ApiTransactionModelMapper();
			var model = new ApiTransactionClientRequestModel();
			model.SetProperties(1m, 1, DateTime.Parse("1/1/1987 12:00:00 AM"), "A");
			ApiTransactionClientResponseModel response = mapper.MapClientRequestToResponse(1, model);
			response.Should().NotBeNull();
			response.Amount.Should().Be(1m);
			response.ArtistId.Should().Be(1);
			response.DateCreated.Should().Be(DateTime.Parse("1/1/1987 12:00:00 AM"));
			response.StripeTransactionId.Should().Be("A");
		}

		[Fact]
		public void MapClientResponseToRequest()
		{
			var mapper = new ApiTransactionModelMapper();
			var model = new ApiTransactionClientResponseModel();
			model.SetProperties(1, 1m, 1, DateTime.Parse("1/1/1987 12:00:00 AM"), "A");
			ApiTransactionClientRequestModel response = mapper.MapClientResponseToRequest(model);
			response.Should().NotBeNull();
			response.Amount.Should().Be(1m);
			response.ArtistId.Should().Be(1);
			response.DateCreated.Should().Be(DateTime.Parse("1/1/1987 12:00:00 AM"));
			response.StripeTransactionId.Should().Be("A");
		}
	}
}

/*<Codenesium>
    <Hash>2ac6070a5d13284a883930d1f048c2e6</Hash>
</Codenesium>*/