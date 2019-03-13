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
	public partial class EmailRepository : AbstractEmailRepository, IEmailRepository
	{
		public EmailRepository(
			ILogger<EmailRepository> logger,
			ApplicationDbContext context)
			: base(logger, context)
		{
		}
	}
}

/*<Codenesium>
    <Hash>6b8a5ab8b58679a5bda0c8590d6cb99e</Hash>
</Codenesium>*/