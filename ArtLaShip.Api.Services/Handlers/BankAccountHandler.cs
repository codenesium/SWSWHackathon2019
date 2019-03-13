using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public class BankAccountCreatedHandler : INotificationHandler<BankAccountCreatedNotification>
	{
		public async Task Handle(BankAccountCreatedNotification notification, CancellationToken cancellation)
		{
			await Task.CompletedTask;
		}
	}

	public class BankAccountUpdatedHandler : INotificationHandler<BankAccountUpdatedNotification>
	{
		public async Task Handle(BankAccountUpdatedNotification notification, CancellationToken cancellation)
		{
			await Task.CompletedTask;
		}
	}

	public class BankAccountDeletedHandler : INotificationHandler<BankAccountDeletedNotification>
	{
		public async Task Handle(BankAccountDeletedNotification notification, CancellationToken cancellation)
		{
			await Task.CompletedTask;
		}
	}

	public class BankAccountCreatedNotification : INotification
	{
		public ApiBankAccountServerResponseModel Record { get; private set; }

		public BankAccountCreatedNotification(ApiBankAccountServerResponseModel record)
		{
			this.Record = record;
		}
	}

	public class BankAccountUpdatedNotification : INotification
	{
		public ApiBankAccountServerResponseModel Record { get; private set; }

		public BankAccountUpdatedNotification(ApiBankAccountServerResponseModel record)
		{
			this.Record = record;
		}
	}

	public class BankAccountDeletedNotification : INotification
	{
		public int Id { get; private set; }

		public BankAccountDeletedNotification(int id)
		{
			this.Id = id;
		}
	}
}

/*<Codenesium>
    <Hash>6210676d8f886a142dc94e0791c73cc7</Hash>
</Codenesium>*/