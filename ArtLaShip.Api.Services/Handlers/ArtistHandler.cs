using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.Services
{
	public class ArtistCreatedHandler : INotificationHandler<ArtistCreatedNotification>
	{
		public async Task Handle(ArtistCreatedNotification notification, CancellationToken cancellation)
		{
			await Task.CompletedTask;
		}
	}

	public class ArtistUpdatedHandler : INotificationHandler<ArtistUpdatedNotification>
	{
		public async Task Handle(ArtistUpdatedNotification notification, CancellationToken cancellation)
		{
			await Task.CompletedTask;
		}
	}

	public class ArtistDeletedHandler : INotificationHandler<ArtistDeletedNotification>
	{
		public async Task Handle(ArtistDeletedNotification notification, CancellationToken cancellation)
		{
			await Task.CompletedTask;
		}
	}

	public class ArtistCreatedNotification : INotification
	{
		public ApiArtistServerResponseModel Record { get; private set; }

		public ArtistCreatedNotification(ApiArtistServerResponseModel record)
		{
			this.Record = record;
		}
	}

	public class ArtistUpdatedNotification : INotification
	{
		public ApiArtistServerResponseModel Record { get; private set; }

		public ArtistUpdatedNotification(ApiArtistServerResponseModel record)
		{
			this.Record = record;
		}
	}

	public class ArtistDeletedNotification : INotification
	{
		public int Id { get; private set; }

		public ArtistDeletedNotification(int id)
		{
			this.Id = id;
		}
	}
}

/*<Codenesium>
    <Hash>04c51a92f7502c2518c3b962769c9548</Hash>
</Codenesium>*/