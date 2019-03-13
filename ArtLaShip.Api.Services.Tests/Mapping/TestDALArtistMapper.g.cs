using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace ArtLaShipNS.Api.Services
{
	[Trait("Type", "Unit")]
	[Trait("Table", "Artist")]
	[Trait("Area", "DALMapper")]
	public class TestDALArtistMapper
	{
		[Fact]
		public void MapModelToEntity()
		{
			var mapper = new DALArtistMapper();
			ApiArtistServerRequestModel model = new ApiArtistServerRequestModel();
			model.SetProperties("A", "A", Guid.Parse("8420cdcf-d595-ef65-66e7-dff9f98764da"), "A", "A", "A", "A", "A");
			Artist response = mapper.MapModelToEntity(1, model);

			response.AspNetUserId.Should().Be("A");
			response.Bio.Should().Be("A");
			response.ExternalId.Should().Be(Guid.Parse("8420cdcf-d595-ef65-66e7-dff9f98764da"));
			response.Facebook.Should().Be("A");
			response.Name.Should().Be("A");
			response.SoundCloud.Should().Be("A");
			response.Twitter.Should().Be("A");
			response.Website.Should().Be("A");
		}

		[Fact]
		public void MapEntityToModel()
		{
			var mapper = new DALArtistMapper();
			Artist item = new Artist();
			item.SetProperties(1, "A", "A", Guid.Parse("8420cdcf-d595-ef65-66e7-dff9f98764da"), "A", "A", "A", "A", "A");
			ApiArtistServerResponseModel response = mapper.MapEntityToModel(item);

			response.AspNetUserId.Should().Be("A");
			response.Bio.Should().Be("A");
			response.ExternalId.Should().Be(Guid.Parse("8420cdcf-d595-ef65-66e7-dff9f98764da"));
			response.Facebook.Should().Be("A");
			response.Id.Should().Be(1);
			response.Name.Should().Be("A");
			response.SoundCloud.Should().Be("A");
			response.Twitter.Should().Be("A");
			response.Website.Should().Be("A");
		}

		[Fact]
		public void MapEntityToModelList()
		{
			var mapper = new DALArtistMapper();
			Artist item = new Artist();
			item.SetProperties(1, "A", "A", Guid.Parse("8420cdcf-d595-ef65-66e7-dff9f98764da"), "A", "A", "A", "A", "A");
			List<ApiArtistServerResponseModel> response = mapper.MapEntityToModel(new List<Artist>() { { item} });

			response.Count.Should().Be(1);
		}
	}
}

/*<Codenesium>
    <Hash>b8dc3382b7d24821d8f15463a09af621</Hash>
</Codenesium>*/