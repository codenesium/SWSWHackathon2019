using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ArtLaShipNS.Api.Services
{
	public partial class ArtistService : AbstractArtistService, IArtistService
	{
		public ArtistService(
			ILogger<IArtistRepository> logger,
			IMediator mediator,
			IArtistRepository artistRepository,
			IApiArtistServerRequestModelValidator artistModelValidator,
			IDALArtistMapper dalArtistMapper,
			IDALBankAccountMapper dalBankAccountMapper,
			IDALEmailMapper dalEmailMapper,
			IDALTransactionMapper dalTransactionMapper)
			: base(logger,
			       mediator,
			       artistRepository,
			       artistModelValidator,
			       dalArtistMapper,
			       dalBankAccountMapper,
			       dalEmailMapper,
			       dalTransactionMapper)
		{
		}
	}
}

/*<Codenesium>
    <Hash>d52c74273787097763613e605ec9a3ca</Hash>
</Codenesium>*/