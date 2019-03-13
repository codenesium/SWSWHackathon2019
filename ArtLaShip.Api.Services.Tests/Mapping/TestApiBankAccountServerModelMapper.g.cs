using FluentAssertions;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using Xunit;

namespace ArtLaShipNS.Api.Services.Tests
{
	[Trait("Type", "Unit")]
	[Trait("Table", "BankAccount")]
	[Trait("Area", "ApiModel")]
	public class TestApiBankAccountServerModelMapper
	{
		[Fact]
		public void MapServerRequestToResponse()
		{
			var mapper = new ApiBankAccountServerModelMapper();
			var model = new ApiBankAccountServerRequestModel();
			model.SetProperties("A", 1, "A");
			ApiBankAccountServerResponseModel response = mapper.MapServerRequestToResponse(1, model);
			response.Should().NotBeNull();
			response.AccountNumber.Should().Be("A");
			response.ArtistId.Should().Be(1);
			response.RoutingNumber.Should().Be("A");
		}

		[Fact]
		public void MapServerResponseToRequest()
		{
			var mapper = new ApiBankAccountServerModelMapper();
			var model = new ApiBankAccountServerResponseModel();
			model.SetProperties(1, "A", 1, "A");
			ApiBankAccountServerRequestModel response = mapper.MapServerResponseToRequest(model);
			response.Should().NotBeNull();
			response.AccountNumber.Should().Be("A");
			response.ArtistId.Should().Be(1);
			response.RoutingNumber.Should().Be("A");
		}

		[Fact]
		public void CreatePatch()
		{
			var mapper = new ApiBankAccountServerModelMapper();
			var model = new ApiBankAccountServerRequestModel();
			model.SetProperties("A", 1, "A");

			JsonPatchDocument<ApiBankAccountServerRequestModel> patch = mapper.CreatePatch(model);
			var response = new ApiBankAccountServerRequestModel();
			patch.ApplyTo(response);
			response.AccountNumber.Should().Be("A");
			response.ArtistId.Should().Be(1);
			response.RoutingNumber.Should().Be("A");
		}
	}
}

/*<Codenesium>
    <Hash>40b155458157b83650cc6312d6f4b8f6</Hash>
</Codenesium>*/