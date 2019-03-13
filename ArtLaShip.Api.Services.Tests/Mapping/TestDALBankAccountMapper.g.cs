using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace ArtLaShipNS.Api.Services
{
	[Trait("Type", "Unit")]
	[Trait("Table", "BankAccount")]
	[Trait("Area", "DALMapper")]
	public class TestDALBankAccountMapper
	{
		[Fact]
		public void MapModelToEntity()
		{
			var mapper = new DALBankAccountMapper();
			ApiBankAccountServerRequestModel model = new ApiBankAccountServerRequestModel();
			model.SetProperties("A", 1, "A");
			BankAccount response = mapper.MapModelToEntity(1, model);

			response.AccountNumber.Should().Be("A");
			response.ArtistId.Should().Be(1);
			response.RoutingNumber.Should().Be("A");
		}

		[Fact]
		public void MapEntityToModel()
		{
			var mapper = new DALBankAccountMapper();
			BankAccount item = new BankAccount();
			item.SetProperties(1, "A", 1, "A");
			ApiBankAccountServerResponseModel response = mapper.MapEntityToModel(item);

			response.AccountNumber.Should().Be("A");
			response.ArtistId.Should().Be(1);
			response.Id.Should().Be(1);
			response.RoutingNumber.Should().Be("A");
		}

		[Fact]
		public void MapEntityToModelList()
		{
			var mapper = new DALBankAccountMapper();
			BankAccount item = new BankAccount();
			item.SetProperties(1, "A", 1, "A");
			List<ApiBankAccountServerResponseModel> response = mapper.MapEntityToModel(new List<BankAccount>() { { item} });

			response.Count.Should().Be(1);
		}
	}
}

/*<Codenesium>
    <Hash>7542f4c590cd1f29a957648962d91305</Hash>
</Codenesium>*/