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
	public partial class ArtistRepository : AbstractArtistRepository, IArtistRepository
	{
		public ArtistRepository(
			ILogger<ArtistRepository> logger,
			ApplicationDbContext context)
			: base(logger, context)
		{
		}
	}
}

/*<Codenesium>
    <Hash>ce4dc37f9f3d9ef8882eece59239111d</Hash>
</Codenesium>*/