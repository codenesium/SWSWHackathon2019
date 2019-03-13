using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace ArtLaShipNS.Api.Client.Tests
{
	[Trait("Type", "Unit")]
	[Trait("Table", "Artist")]
	[Trait("Area", "ApiModel")]
	public class TestApiArtistModelMapper
	{
		[Fact]
		public void MapClientRequestToResponse()
		{
			var mapper = new ApiArtistModelMapper();
			var model = new ApiArtistClientRequestModel();
			model.SetProperties("A", "A", Guid.Parse("8420cdcf-d595-ef65-66e7-dff9f98764da"), "A", "A", "A", "A", "A", "A");
			ApiArtistClientResponseModel response = mapper.MapClientRequestToResponse(1, model);
			response.Should().NotBeNull();
			response.AspNetUserId.Should().Be("A");
			response.Bio.Should().Be("A");
			response.ExternalId.Should().Be(Guid.Parse("8420cdcf-d595-ef65-66e7-dff9f98764da"));
			response.Facebook.Should().Be("A");
			response.Name.Should().Be("A");
			response.SoundCloud.Should().Be("A");
			response.Twitter.Should().Be("A");
			response.Venmo.Should().Be("A");
			response.Website.Should().Be("A");
		}

		[Fact]
		public void MapClientResponseToRequest()
		{
			var mapper = new ApiArtistModelMapper();
			var model = new ApiArtistClientResponseModel();
			model.SetProperties(1, "A", "A", Guid.Parse("8420cdcf-d595-ef65-66e7-dff9f98764da"), "A", "A", "A", "A", "A", "A");
			ApiArtistClientRequestModel response = mapper.MapClientResponseToRequest(model);
			response.Should().NotBeNull();
			response.AspNetUserId.Should().Be("A");
			response.Bio.Should().Be("A");
			response.ExternalId.Should().Be(Guid.Parse("8420cdcf-d595-ef65-66e7-dff9f98764da"));
			response.Facebook.Should().Be("A");
			response.Name.Should().Be("A");
			response.SoundCloud.Should().Be("A");
			response.Twitter.Should().Be("A");
			response.Venmo.Should().Be("A");
			response.Website.Should().Be("A");
		}
	}
}

/*<Codenesium>
    <Hash>158d286bda55b072fdc6a198cebbb2c6</Hash>
</Codenesium>*/