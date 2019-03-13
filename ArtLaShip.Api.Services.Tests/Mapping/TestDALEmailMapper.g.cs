using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace ArtLaShipNS.Api.Services
{
	[Trait("Type", "Unit")]
	[Trait("Table", "Email")]
	[Trait("Area", "DALMapper")]
	public class TestDALEmailMapper
	{
		[Fact]
		public void MapModelToEntity()
		{
			var mapper = new DALEmailMapper();
			ApiEmailServerRequestModel model = new ApiEmailServerRequestModel();
			model.SetProperties(1, DateTime.Parse("1/1/1987 12:00:00 AM"), "A");
			Email response = mapper.MapModelToEntity(1, model);

			response.ArtistId.Should().Be(1);
			response.DateCreated.Should().Be(DateTime.Parse("1/1/1987 12:00:00 AM"));
			response.EmailValue.Should().Be("A");
		}

		[Fact]
		public void MapEntityToModel()
		{
			var mapper = new DALEmailMapper();
			Email item = new Email();
			item.SetProperties(1, 1, DateTime.Parse("1/1/1987 12:00:00 AM"), "A");
			ApiEmailServerResponseModel response = mapper.MapEntityToModel(item);

			response.ArtistId.Should().Be(1);
			response.DateCreated.Should().Be(DateTime.Parse("1/1/1987 12:00:00 AM"));
			response.EmailValue.Should().Be("A");
			response.Id.Should().Be(1);
		}

		[Fact]
		public void MapEntityToModelList()
		{
			var mapper = new DALEmailMapper();
			Email item = new Email();
			item.SetProperties(1, 1, DateTime.Parse("1/1/1987 12:00:00 AM"), "A");
			List<ApiEmailServerResponseModel> response = mapper.MapEntityToModel(new List<Email>() { { item} });

			response.Count.Should().Be(1);
		}
	}
}

/*<Codenesium>
    <Hash>5011fd33cefc529ebb0385307912b5f0</Hash>
</Codenesium>*/