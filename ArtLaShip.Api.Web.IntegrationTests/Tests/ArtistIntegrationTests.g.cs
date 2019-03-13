using ArtLaShipNS.Api.Client;
using ArtLaShipNS.Api.Contracts;
using ArtLaShipNS.Api.DataAccess;
using ArtLaShipNS.Api.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ArtLaShipNS.Api.Web.IntegrationTests
{
	[Trait("Type", "Integration")]
	[Trait("Table", "Artist")]
	[Trait("Area", "Integration")]
	public partial class ArtistIntegrationTests
	{
		public ArtistIntegrationTests()
		{
		}

		[Fact]
		public virtual async void TestBulkInsert()
		{
			var builder = new WebHostBuilder()
			              .UseEnvironment("Production")
			              .UseStartup<TestStartup>();
			TestServer testServer = new TestServer(builder);
			var client = new ApiClient(testServer.CreateClient());
			ApplicationDbContext context = testServer.Host.Services.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;

			var model = new ApiArtistClientRequestModel();
			model.SetProperties("B", "B", Guid.Parse("3842cac4-b9a0-8223-0dcc-509a6f75849b"), "B", "B", "B", "B", "B");
			var model2 = new ApiArtistClientRequestModel();
			model2.SetProperties("C", "C", Guid.Parse("8d721ec8-4c9d-632f-6f06-7f89cc14862c"), "C", "C", "C", "C", "C");
			var request = new List<ApiArtistClientRequestModel>() {model, model2};
			CreateResponse<List<ApiArtistClientResponseModel>> result = await client.ArtistBulkInsertAsync(request);

			result.Success.Should().BeTrue();
			result.Record.Should().NotBeNull();

			context.Set<Artist>().ToList()[1].AspNetUserId.Should().Be("B");
			context.Set<Artist>().ToList()[1].Bio.Should().Be("B");
			context.Set<Artist>().ToList()[1].ExternalId.Should().Be(Guid.Parse("3842cac4-b9a0-8223-0dcc-509a6f75849b"));
			context.Set<Artist>().ToList()[1].Facebook.Should().Be("B");
			context.Set<Artist>().ToList()[1].Name.Should().Be("B");
			context.Set<Artist>().ToList()[1].SoundCloud.Should().Be("B");
			context.Set<Artist>().ToList()[1].Twitter.Should().Be("B");
			context.Set<Artist>().ToList()[1].Website.Should().Be("B");

			context.Set<Artist>().ToList()[2].AspNetUserId.Should().Be("C");
			context.Set<Artist>().ToList()[2].Bio.Should().Be("C");
			context.Set<Artist>().ToList()[2].ExternalId.Should().Be(Guid.Parse("8d721ec8-4c9d-632f-6f06-7f89cc14862c"));
			context.Set<Artist>().ToList()[2].Facebook.Should().Be("C");
			context.Set<Artist>().ToList()[2].Name.Should().Be("C");
			context.Set<Artist>().ToList()[2].SoundCloud.Should().Be("C");
			context.Set<Artist>().ToList()[2].Twitter.Should().Be("C");
			context.Set<Artist>().ToList()[2].Website.Should().Be("C");
		}

		[Fact]
		public virtual async void TestCreate()
		{
			var builder = new WebHostBuilder()
			              .UseEnvironment("Production")
			              .UseStartup<TestStartup>();
			TestServer testServer = new TestServer(builder);
			var client = new ApiClient(testServer.CreateClient());
			ApplicationDbContext context = testServer.Host.Services.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;

			var model = new ApiArtistClientRequestModel();
			model.SetProperties("B", "B", Guid.Parse("3842cac4-b9a0-8223-0dcc-509a6f75849b"), "B", "B", "B", "B", "B");
			CreateResponse<ApiArtistClientResponseModel> result = await client.ArtistCreateAsync(model);

			result.Success.Should().BeTrue();
			result.Record.Should().NotBeNull();
			context.Set<Artist>().ToList()[1].AspNetUserId.Should().Be("B");
			context.Set<Artist>().ToList()[1].Bio.Should().Be("B");
			context.Set<Artist>().ToList()[1].ExternalId.Should().Be(Guid.Parse("3842cac4-b9a0-8223-0dcc-509a6f75849b"));
			context.Set<Artist>().ToList()[1].Facebook.Should().Be("B");
			context.Set<Artist>().ToList()[1].Name.Should().Be("B");
			context.Set<Artist>().ToList()[1].SoundCloud.Should().Be("B");
			context.Set<Artist>().ToList()[1].Twitter.Should().Be("B");
			context.Set<Artist>().ToList()[1].Website.Should().Be("B");

			result.Record.AspNetUserId.Should().Be("B");
			result.Record.Bio.Should().Be("B");
			result.Record.ExternalId.Should().Be(Guid.Parse("3842cac4-b9a0-8223-0dcc-509a6f75849b"));
			result.Record.Facebook.Should().Be("B");
			result.Record.Name.Should().Be("B");
			result.Record.SoundCloud.Should().Be("B");
			result.Record.Twitter.Should().Be("B");
			result.Record.Website.Should().Be("B");
		}

		[Fact]
		public virtual async void TestUpdate()
		{
			var builder = new WebHostBuilder()
			              .UseEnvironment("Production")
			              .UseStartup<TestStartup>();
			TestServer testServer = new TestServer(builder);

			var client = new ApiClient(testServer.CreateClient());
			var mapper = new ApiArtistServerModelMapper();
			ApplicationDbContext context = testServer.Host.Services.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
			IArtistService service = testServer.Host.Services.GetService(typeof(IArtistService)) as IArtistService;
			ApiArtistServerResponseModel model = await service.Get(1);

			ApiArtistClientRequestModel request = mapper.MapServerResponseToClientRequest(model);
			request.SetProperties("B", "B", Guid.Parse("3842cac4-b9a0-8223-0dcc-509a6f75849b"), "B", "B", "B", "B", "B");

			UpdateResponse<ApiArtistClientResponseModel> updateResponse = await client.ArtistUpdateAsync(model.Id, request);

			context.Entry(context.Set<Artist>().ToList()[0]).Reload();
			updateResponse.Record.Should().NotBeNull();
			updateResponse.Success.Should().BeTrue();
			updateResponse.Record.Id.Should().Be(1);
			context.Set<Artist>().ToList()[0].AspNetUserId.Should().Be("B");
			context.Set<Artist>().ToList()[0].Bio.Should().Be("B");
			context.Set<Artist>().ToList()[0].ExternalId.Should().Be(Guid.Parse("3842cac4-b9a0-8223-0dcc-509a6f75849b"));
			context.Set<Artist>().ToList()[0].Facebook.Should().Be("B");
			context.Set<Artist>().ToList()[0].Name.Should().Be("B");
			context.Set<Artist>().ToList()[0].SoundCloud.Should().Be("B");
			context.Set<Artist>().ToList()[0].Twitter.Should().Be("B");
			context.Set<Artist>().ToList()[0].Website.Should().Be("B");

			updateResponse.Record.Id.Should().Be(1);
			updateResponse.Record.AspNetUserId.Should().Be("B");
			updateResponse.Record.Bio.Should().Be("B");
			updateResponse.Record.ExternalId.Should().Be(Guid.Parse("3842cac4-b9a0-8223-0dcc-509a6f75849b"));
			updateResponse.Record.Facebook.Should().Be("B");
			updateResponse.Record.Name.Should().Be("B");
			updateResponse.Record.SoundCloud.Should().Be("B");
			updateResponse.Record.Twitter.Should().Be("B");
			updateResponse.Record.Website.Should().Be("B");
		}

		[Fact]
		public virtual async void TestDelete()
		{
			var builder = new WebHostBuilder()
			              .UseEnvironment("Production")
			              .UseStartup<TestStartup>();
			TestServer testServer = new TestServer(builder);
			var client = new ApiClient(testServer.CreateClient());
			ApplicationDbContext context = testServer.Host.Services.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;

			IArtistService service = testServer.Host.Services.GetService(typeof(IArtistService)) as IArtistService;
			var model = new ApiArtistServerRequestModel();
			model.SetProperties("B", "B", Guid.Parse("3842cac4-b9a0-8223-0dcc-509a6f75849b"), "B", "B", "B", "B", "B");
			CreateResponse<ApiArtistServerResponseModel> createdResponse = await service.Create(model);

			createdResponse.Success.Should().BeTrue();

			ActionResponse deleteResult = await client.ArtistDeleteAsync(2);

			deleteResult.Success.Should().BeTrue();
			ApiArtistServerResponseModel verifyResponse = await service.Get(2);

			verifyResponse.Should().BeNull();
		}

		[Fact]
		public virtual async void TestGetFound()
		{
			var builder = new WebHostBuilder()
			              .UseEnvironment("Production")
			              .UseStartup<TestStartup>();
			TestServer testServer = new TestServer(builder);

			var client = new ApiClient(testServer.CreateClient());
			ApplicationDbContext context = testServer.Host.Services.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;

			ApiArtistClientResponseModel response = await client.ArtistGetAsync(1);

			response.Should().NotBeNull();
			response.AspNetUserId.Should().Be("A");
			response.Bio.Should().Be("A");
			response.ExternalId.Should().Be(Guid.Parse("8420cdcf-d595-ef65-66e7-dff9f98764da"));
			response.Facebook.Should().Be("A");
			response.Id.Should().Be(1);
			response.Name.Should().Be("A");
			response.SoundCloud.Should().Be("A");
			response.Twitter.Should().Be("A");
			response.Website.Should().Be("A");
		}

		[Fact]
		public virtual async void TestGetNotFound()
		{
			var builder = new WebHostBuilder()
			              .UseEnvironment("Production")
			              .UseStartup<TestStartup>();
			TestServer testServer = new TestServer(builder);

			var client = new ApiClient(testServer.CreateClient());
			ApiArtistClientResponseModel response = await client.ArtistGetAsync(default(int));

			response.Should().BeNull();
		}

		[Fact]
		public virtual async void TestAll()
		{
			var builder = new WebHostBuilder()
			              .UseEnvironment("Production")
			              .UseStartup<TestStartup>();
			TestServer testServer = new TestServer(builder);

			var client = new ApiClient(testServer.CreateClient());

			List<ApiArtistClientResponseModel> response = await client.ArtistAllAsync();

			response.Count.Should().BeGreaterThan(0);
			response[0].AspNetUserId.Should().Be("A");
			response[0].Bio.Should().Be("A");
			response[0].ExternalId.Should().Be(Guid.Parse("8420cdcf-d595-ef65-66e7-dff9f98764da"));
			response[0].Facebook.Should().Be("A");
			response[0].Id.Should().Be(1);
			response[0].Name.Should().Be("A");
			response[0].SoundCloud.Should().Be("A");
			response[0].Twitter.Should().Be("A");
			response[0].Website.Should().Be("A");
		}

		[Fact]
		public virtual async void TestForeignKeyBankAccountsByArtistIdFound()
		{
			var builder = new WebHostBuilder()
			              .UseEnvironment("Production")
			              .UseStartup<TestStartup>();
			TestServer testServer = new TestServer(builder);

			var client = new ApiClient(testServer.CreateClient());
			List<ApiBankAccountClientResponseModel> response = await client.BankAccountsByArtistId(1);

			response.Should().NotBeEmpty();
		}

		[Fact]
		public virtual async void TestForeignKeyBankAccountsByArtistIdNotFound()
		{
			var builder = new WebHostBuilder()
			              .UseEnvironment("Production")
			              .UseStartup<TestStartup>();
			TestServer testServer = new TestServer(builder);

			var client = new ApiClient(testServer.CreateClient());
			List<ApiBankAccountClientResponseModel> response = await client.BankAccountsByArtistId(default(int));

			response.Should().BeEmpty();
		}

		[Fact]
		public virtual async void TestForeignKeyEmailsByArtistIdFound()
		{
			var builder = new WebHostBuilder()
			              .UseEnvironment("Production")
			              .UseStartup<TestStartup>();
			TestServer testServer = new TestServer(builder);

			var client = new ApiClient(testServer.CreateClient());
			List<ApiEmailClientResponseModel> response = await client.EmailsByArtistId(1);

			response.Should().NotBeEmpty();
		}

		[Fact]
		public virtual async void TestForeignKeyEmailsByArtistIdNotFound()
		{
			var builder = new WebHostBuilder()
			              .UseEnvironment("Production")
			              .UseStartup<TestStartup>();
			TestServer testServer = new TestServer(builder);

			var client = new ApiClient(testServer.CreateClient());
			List<ApiEmailClientResponseModel> response = await client.EmailsByArtistId(default(int));

			response.Should().BeEmpty();
		}

		[Fact]
		public virtual async void TestForeignKeyTransactionsByArtistIdFound()
		{
			var builder = new WebHostBuilder()
			              .UseEnvironment("Production")
			              .UseStartup<TestStartup>();
			TestServer testServer = new TestServer(builder);

			var client = new ApiClient(testServer.CreateClient());
			List<ApiTransactionClientResponseModel> response = await client.TransactionsByArtistId(1);

			response.Should().NotBeEmpty();
		}

		[Fact]
		public virtual async void TestForeignKeyTransactionsByArtistIdNotFound()
		{
			var builder = new WebHostBuilder()
			              .UseEnvironment("Production")
			              .UseStartup<TestStartup>();
			TestServer testServer = new TestServer(builder);

			var client = new ApiClient(testServer.CreateClient());
			List<ApiTransactionClientResponseModel> response = await client.TransactionsByArtistId(default(int));

			response.Should().BeEmpty();
		}

		[Fact]
		public virtual void TestClientCancellationToken()
		{
			Func<Task> testCancellation = async () =>
			{
				var builder = new WebHostBuilder()
				              .UseEnvironment("Production")
				              .UseStartup<TestStartup>();
				TestServer testServer = new TestServer(builder);

				var client = new ApiClient(testServer.BaseAddress.OriginalString);
				CancellationTokenSource tokenSource = new CancellationTokenSource();
				CancellationToken token = tokenSource.Token;
				tokenSource.Cancel();
				var result = await client.ArtistAllAsync(token);
			};

			testCancellation.Should().Throw<OperationCanceledException>();
		}
	}
}

/*<Codenesium>
    <Hash>7c4824c15001cd199c5dc40e7d1a3d19</Hash>
</Codenesium>*/