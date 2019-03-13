using FluentAssertions;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using Xunit;

namespace ArtLaShipNS.Api.Services.Tests
{
	[Trait("Type", "Unit")]
	[Trait("Table", "Email")]
	[Trait("Area", "ApiModel")]
	public class TestApiEmailServerModelMapper
	{
		[Fact]
		public void MapServerRequestToResponse()
		{
			var mapper = new ApiEmailServerModelMapper();
			var model = new ApiEmailServerRequestModel();
			model.SetProperties(1, DateTime.Parse("1/1/1987 12:00:00 AM"), "A");
			ApiEmailServerResponseModel response = mapper.MapServerRequestToResponse(1, model);
			response.Should().NotBeNull();
			response.ArtistId.Should().Be(1);
			response.DateCreated.Should().Be(DateTime.Parse("1/1/1987 12:00:00 AM"));
			response.EmailValue.Should().Be("A");
		}

		[Fact]
		public void MapServerResponseToRequest()
		{
			var mapper = new ApiEmailServerModelMapper();
			var model = new ApiEmailServerResponseModel();
			model.SetProperties(1, 1, DateTime.Parse("1/1/1987 12:00:00 AM"), "A");
			ApiEmailServerRequestModel response = mapper.MapServerResponseToRequest(model);
			response.Should().NotBeNull();
			response.ArtistId.Should().Be(1);
			response.DateCreated.Should().Be(DateTime.Parse("1/1/1987 12:00:00 AM"));
			response.EmailValue.Should().Be("A");
		}

		[Fact]
		public void CreatePatch()
		{
			var mapper = new ApiEmailServerModelMapper();
			var model = new ApiEmailServerRequestModel();
			model.SetProperties(1, DateTime.Parse("1/1/1987 12:00:00 AM"), "A");

			JsonPatchDocument<ApiEmailServerRequestModel> patch = mapper.CreatePatch(model);
			var response = new ApiEmailServerRequestModel();
			patch.ApplyTo(response);
			response.ArtistId.Should().Be(1);
			response.DateCreated.Should().Be(DateTime.Parse("1/1/1987 12:00:00 AM"));
			response.EmailValue.Should().Be("A");
		}
	}
}

/*<Codenesium>
    <Hash>37ca63de058a420193aa7e6ce4b92c18</Hash>
</Codenesium>*/