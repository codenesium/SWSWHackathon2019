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
	public partial class TransactionRepository : AbstractTransactionRepository, ITransactionRepository
	{
		public TransactionRepository(
			ILogger<TransactionRepository> logger,
			ApplicationDbContext context)
			: base(logger, context)
		{
		}
	}
}

/*<Codenesium>
    <Hash>8717bb222ce09cdd9dc3f8f41aaee0ee</Hash>
</Codenesium>*/