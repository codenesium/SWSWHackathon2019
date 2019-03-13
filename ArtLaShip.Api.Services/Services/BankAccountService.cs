using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ArtLaShipNS.Api.Services
{
	public partial class BankAccountService : AbstractBankAccountService, IBankAccountService
	{
		public BankAccountService(
			ILogger<IBankAccountRepository> logger,
			IMediator mediator,
			IBankAccountRepository bankAccountRepository,
			IApiBankAccountServerRequestModelValidator bankAccountModelValidator,
			IDALBankAccountMapper dalBankAccountMapper)
			: base(logger,
			       mediator,
			       bankAccountRepository,
			       bankAccountModelValidator,
			       dalBankAccountMapper)
		{
		}
	}
}

/*<Codenesium>
    <Hash>18c967ed4769518351ef618b3e731603</Hash>
</Codenesium>*/