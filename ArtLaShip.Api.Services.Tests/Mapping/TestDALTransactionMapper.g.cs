using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace ArtLaShipNS.Api.Services
{
	[Trait("Type", "Unit")]
	[Trait("Table", "Transaction")]
	[Trait("Area", "DALMapper")]
	public class TestDALTransactionMapper
	{
		[Fact]
		public void MapModelToEntity()
		{
			var mapper = new DALTransactionMapper();
			ApiTransactionServerRequestModel model = new ApiTransactionServerRequestModel();
			model.SetProperties(1m, 1, DateTime.Parse("1/1/1987 12:00:00 AM"), "A");
			Transaction response = mapper.MapModelToEntity(1, model);

			response.Amount.Should().Be(1m);
			response.ArtistId.Should().Be(1);
			response.DateCreated.Should().Be(DateTime.Parse("1/1/1987 12:00:00 AM"));
			response.StripeTransactionId.Should().Be("A");
		}

		[Fact]
		public void MapEntityToModel()
		{
			var mapper = new DALTransactionMapper();
			Transaction item = new Transaction();
			item.SetProperties(1, 1m, 1, DateTime.Parse("1/1/1987 12:00:00 AM"), "A");
			ApiTransactionServerResponseModel response = mapper.MapEntityToModel(item);

			response.Amount.Should().Be(1m);
			response.ArtistId.Should().Be(1);
			response.DateCreated.Should().Be(DateTime.Parse("1/1/1987 12:00:00 AM"));
			response.Id.Should().Be(1);
			response.StripeTransactionId.Should().Be("A");
		}

		[Fact]
		public void MapEntityToModelList()
		{
			var mapper = new DALTransactionMapper();
			Transaction item = new Transaction();
			item.SetProperties(1, 1m, 1, DateTime.Parse("1/1/1987 12:00:00 AM"), "A");
			List<ApiTransactionServerResponseModel> response = mapper.MapEntityToModel(new List<Transaction>() { { item} });

			response.Count.Should().Be(1);
		}
	}
}

/*<Codenesium>
    <Hash>f2d900ba9e5b365fb31e5e4e4eaec24c</Hash>
</Codenesium>*/