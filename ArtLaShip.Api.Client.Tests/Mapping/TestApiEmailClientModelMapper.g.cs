using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace ArtLaShipNS.Api.Client.Tests
{
	[Trait("Type", "Unit")]
	[Trait("Table", "Email")]
	[Trait("Area", "ApiModel")]
	public class TestApiEmailModelMapper
	{
		[Fact]
		public void MapClientRequestToResponse()
		{
			var mapper = new ApiEmailModelMapper();
			var model = new ApiEmailClientRequestModel();
			model.SetProperties(1, DateTime.Parse("1/1/1987 12:00:00 AM"), "A");
			ApiEmailClientResponseModel response = mapper.MapClientRequestToResponse(1, model);
			response.Should().NotBeNull();
			response.ArtistId.Should().Be(1);
			response.DateCreated.Should().Be(DateTime.Parse("1/1/1987 12:00:00 AM"));
			response.EmailValue.Should().Be("A");
		}

		[Fact]
		public void MapClientResponseToRequest()
		{
			var mapper = new ApiEmailModelMapper();
			var model = new ApiEmailClientResponseModel();
			model.SetProperties(1, 1, DateTime.Parse("1/1/1987 12:00:00 AM"), "A");
			ApiEmailClientRequestModel response = mapper.MapClientResponseToRequest(model);
			response.Should().NotBeNull();
			response.ArtistId.Should().Be(1);
			response.DateCreated.Should().Be(DateTime.Parse("1/1/1987 12:00:00 AM"));
			response.EmailValue.Should().Be("A");
		}
	}
}

/*<Codenesium>
    <Hash>0e4e790f5c92b6ccc9be15ba2ca3cbef</Hash>
</Codenesium>*/