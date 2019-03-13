using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ArtLaShipNS.Api.Services
{
	public partial class TransactionService : AbstractTransactionService, ITransactionService
	{
		public TransactionService(
			ILogger<ITransactionRepository> logger,
			IMediator mediator,
			ITransactionRepository transactionRepository,
			IApiTransactionServerRequestModelValidator transactionModelValidator,
			IDALTransactionMapper dalTransactionMapper)
			: base(logger,
			       mediator,
			       transactionRepository,
			       transactionModelValidator,
			       dalTransactionMapper)
		{
		}
	}
}

/*<Codenesium>
    <Hash>d3c718417da5ba0f4af108334f96ed45</Hash>
</Codenesium>*/