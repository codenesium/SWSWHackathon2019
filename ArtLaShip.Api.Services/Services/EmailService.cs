using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ArtLaShipNS.Api.Services
{
	public partial class EmailService : AbstractEmailService, IEmailService
	{
		public EmailService(
			ILogger<IEmailRepository> logger,
			IMediator mediator,
			IEmailRepository emailRepository,
			IApiEmailServerRequestModelValidator emailModelValidator,
			IDALEmailMapper dalEmailMapper)
			: base(logger,
			       mediator,
			       emailRepository,
			       emailModelValidator,
			       dalEmailMapper)
		{
		}
	}
}

/*<Codenesium>
    <Hash>b1cfeae505aaed5958e1ad916ae2f821</Hash>
</Codenesium>*/