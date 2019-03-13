using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public class EmailCreatedHandler : INotificationHandler<EmailCreatedNotification>
	{
		public async Task Handle(EmailCreatedNotification notification, CancellationToken cancellation)
		{
			await Task.CompletedTask;
		}
	}

	public class EmailUpdatedHandler : INotificationHandler<EmailUpdatedNotification>
	{
		public async Task Handle(EmailUpdatedNotification notification, CancellationToken cancellation)
		{
			await Task.CompletedTask;
		}
	}

	public class EmailDeletedHandler : INotificationHandler<EmailDeletedNotification>
	{
		public async Task Handle(EmailDeletedNotification notification, CancellationToken cancellation)
		{
			await Task.CompletedTask;
		}
	}

	public class EmailCreatedNotification : INotification
	{
		public ApiEmailServerResponseModel Record { get; private set; }

		public EmailCreatedNotification(ApiEmailServerResponseModel record)
		{
			this.Record = record;
		}
	}

	public class EmailUpdatedNotification : INotification
	{
		public ApiEmailServerResponseModel Record { get; private set; }

		public EmailUpdatedNotification(ApiEmailServerResponseModel record)
		{
			this.Record = record;
		}
	}

	public class EmailDeletedNotification : INotification
	{
		public int Id { get; private set; }

		public EmailDeletedNotification(int id)
		{
			this.Id = id;
		}
	}
}

/*<Codenesium>
    <Hash>34d256a0ece2925a56cfb2c995f2158a</Hash>
</Codenesium>*/