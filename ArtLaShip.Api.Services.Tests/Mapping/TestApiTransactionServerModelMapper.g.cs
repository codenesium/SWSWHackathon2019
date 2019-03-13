using FluentAssertions;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using Xunit;

namespace ArtLaShipNS.Api.Services.Tests
{
	[Trait("Type", "Unit")]
	[Trait("Table", "Transaction")]
	[Trait("Area", "ApiModel")]
	public class TestApiTransactionServerModelMapper
	{
		[Fact]
		public void MapServerRequestToResponse()
		{
			var mapper = new ApiTransactionServerModelMapper();
			var model = new ApiTransactionServerRequestModel();
			model.SetProperties(1m, 1, DateTime.Parse("1/1/1987 12:00:00 AM"), "A");
			ApiTransactionServerResponseModel response = mapper.MapServerRequestToResponse(1, model);
			response.Should().NotBeNull();
			response.Amount.Should().Be(1m);
			response.ArtistId.Should().Be(1);
			response.DateCreated.Should().Be(DateTime.Parse("1/1/1987 12:00:00 AM"));
			response.StripeTransactionId.Should().Be("A");
		}

		[Fact]
		public void MapServerResponseToRequest()
		{
			var mapper = new ApiTransactionServerModelMapper();
			var model = new ApiTransactionServerResponseModel();
			model.SetProperties(1, 1m, 1, DateTime.Parse("1/1/1987 12:00:00 AM"), "A");
			ApiTransactionServerRequestModel response = mapper.MapServerResponseToRequest(model);
			response.Should().NotBeNull();
			response.Amount.Should().Be(1m);
			response.ArtistId.Should().Be(1);
			response.DateCreated.Should().Be(DateTime.Parse("1/1/1987 12:00:00 AM"));
			response.StripeTransactionId.Should().Be("A");
		}

		[Fact]
		public void CreatePatch()
		{
			var mapper = new ApiTransactionServerModelMapper();
			var model = new ApiTransactionServerRequestModel();
			model.SetProperties(1m, 1, DateTime.Parse("1/1/1987 12:00:00 AM"), "A");

			JsonPatchDocument<ApiTransactionServerRequestModel> patch = mapper.CreatePatch(model);
			var response = new ApiTransactionServerRequestModel();
			patch.ApplyTo(response);
			response.Amount.Should().Be(1m);
			response.ArtistId.Should().Be(1);
			response.DateCreated.Should().Be(DateTime.Parse("1/1/1987 12:00:00 AM"));
			response.StripeTransactionId.Should().Be("A");
		}
	}
}

/*<Codenesium>
    <Hash>559371092f5732ec12b9e28017ad3407</Hash>
</Codenesium>*/