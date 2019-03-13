using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace ArtLaShipNS.Api.Client.Tests
{
	[Trait("Type", "Unit")]
	[Trait("Table", "BankAccount")]
	[Trait("Area", "ApiModel")]
	public class TestApiBankAccountModelMapper
	{
		[Fact]
		public void MapClientRequestToResponse()
		{
			var mapper = new ApiBankAccountModelMapper();
			var model = new ApiBankAccountClientRequestModel();
			model.SetProperties("A", 1, "A");
			ApiBankAccountClientResponseModel response = mapper.MapClientRequestToResponse(1, model);
			response.Should().NotBeNull();
			response.AccountNumber.Should().Be("A");
			response.ArtistId.Should().Be(1);
			response.RoutingNumber.Should().Be("A");
		}

		[Fact]
		public void MapClientResponseToRequest()
		{
			var mapper = new ApiBankAccountModelMapper();
			var model = new ApiBankAccountClientResponseModel();
			model.SetProperties(1, "A", 1, "A");
			ApiBankAccountClientRequestModel response = mapper.MapClientResponseToRequest(model);
			response.Should().NotBeNull();
			response.AccountNumber.Should().Be("A");
			response.ArtistId.Should().Be(1);
			response.RoutingNumber.Should().Be("A");
		}
	}
}

/*<Codenesium>
    <Hash>abf52e80410f04cc9bec491a3015e525</Hash>
</Codenesium>*/