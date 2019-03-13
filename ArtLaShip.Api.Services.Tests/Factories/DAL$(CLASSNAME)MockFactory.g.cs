using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using Moq;
using System;
using System.Collections.Generic;

namespace ArtLaShipNS.Api.Services.Tests
{
	public class DALMapperMockFactory
	{
		public IDALArtistMapper DALArtistMapperMock { get; set; } = new DALArtistMapper();

		public IDALBankAccountMapper DALBankAccountMapperMock { get; set; } = new DALBankAccountMapper();

		public IDALTransactionMapper DALTransactionMapperMock { get; set; } = new DALTransactionMapper();

		public IDALEmailMapper DALEmailMapperMock { get; set; } = new DALEmailMapper();

		public DALMapperMockFactory()
		{
		}
	}
}

/*<Codenesium>
    <Hash>e2ff6862afd2e063cd2923e4baccbec3</Hash>
</Codenesium>*/