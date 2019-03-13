using FluentAssertions;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using Xunit;

namespace ArtLaShipNS.Api.Services.Tests
{
	[Trait("Type", "Unit")]
	[Trait("Table", "Artist")]
	[Trait("Area", "ApiModel")]
	public class TestApiArtistServerModelMapper
	{
		[Fact]
		public void MapServerRequestToResponse()
		{
			var mapper = new ApiArtistServerModelMapper();
			var model = new ApiArtistServerRequestModel();
			model.SetProperties("A", "A", Guid.Parse("8420cdcf-d595-ef65-66e7-dff9f98764da"), "A", "A", "A", "A", "A", "A");
			ApiArtistServerResponseModel response = mapper.MapServerRequestToResponse(1, model);
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
		public void MapServerResponseToRequest()
		{
			var mapper = new ApiArtistServerModelMapper();
			var model = new ApiArtistServerResponseModel();
			model.SetProperties(1, "A", "A", Guid.Parse("8420cdcf-d595-ef65-66e7-dff9f98764da"), "A", "A", "A", "A", "A", "A");
			ApiArtistServerRequestModel response = mapper.MapServerResponseToRequest(model);
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
		public void CreatePatch()
		{
			var mapper = new ApiArtistServerModelMapper();
			var model = new ApiArtistServerRequestModel();
			model.SetProperties("A", "A", Guid.Parse("8420cdcf-d595-ef65-66e7-dff9f98764da"), "A", "A", "A", "A", "A", "A");

			JsonPatchDocument<ApiArtistServerRequestModel> patch = mapper.CreatePatch(model);
			var response = new ApiArtistServerRequestModel();
			patch.ApplyTo(response);
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
    <Hash>4cde0413b88cd2d1665971cbb105172d</Hash>
</Codenesium>*/