using Codenesium.DataConversionExtensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.DataAccess
{
	public partial class BankAccountRepository : AbstractBankAccountRepository, IBankAccountRepository
	{
		public BankAccountRepository(
			ILogger<BankAccountRepository> logger,
			ApplicationDbContext context)
			: base(logger, context)
		{
		}
	}
}

/*<Codenesium>
    <Hash>925e29ea52408fcbdec6f1ed1ba11868</Hash>
</Codenesium>*/