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
	[Trait("Table", "BankAccount")]
	[Trait("Area", "Integration")]
	public partial class BankAccountIntegrationTests
	{
		public BankAccountIntegrationTests()
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

			var model = new ApiBankAccountClientRequestModel();
			model.SetProperties("B", 1, "B");
			var model2 = new ApiBankAccountClientRequestModel();
			model2.SetProperties("C", 1, "C");
			var request = new List<ApiBankAccountClientRequestModel>() {model, model2};
			CreateResponse<List<ApiBankAccountClientResponseModel>> result = await client.BankAccountBulkInsertAsync(request);

			result.Success.Should().BeTrue();
			result.Record.Should().NotBeNull();

			context.Set<BankAccount>().ToList()[1].AccountNumber.Should().Be("B");
			context.Set<BankAccount>().ToList()[1].ArtistId.Should().Be(1);
			context.Set<BankAccount>().ToList()[1].RoutingNumber.Should().Be("B");

			context.Set<BankAccount>().ToList()[2].AccountNumber.Should().Be("C");
			context.Set<BankAccount>().ToList()[2].ArtistId.Should().Be(1);
			context.Set<BankAccount>().ToList()[2].RoutingNumber.Should().Be("C");
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

			var model = new ApiBankAccountClientRequestModel();
			model.SetProperties("B", 1, "B");
			CreateResponse<ApiBankAccountClientResponseModel> result = await client.BankAccountCreateAsync(model);

			result.Success.Should().BeTrue();
			result.Record.Should().NotBeNull();
			context.Set<BankAccount>().ToList()[1].AccountNumber.Should().Be("B");
			context.Set<BankAccount>().ToList()[1].ArtistId.Should().Be(1);
			context.Set<BankAccount>().ToList()[1].RoutingNumber.Should().Be("B");

			result.Record.AccountNumber.Should().Be("B");
			result.Record.ArtistId.Should().Be(1);
			result.Record.RoutingNumber.Should().Be("B");
		}

		[Fact]
		public virtual async void TestUpdate()
		{
			var builder = new WebHostBuilder()
			              .UseEnvironment("Production")
			              .UseStartup<TestStartup>();
			TestServer testServer = new TestServer(builder);

			var client = new ApiClient(testServer.CreateClient());
			var mapper = new ApiBankAccountServerModelMapper();
			ApplicationDbContext context = testServer.Host.Services.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
			IBankAccountService service = testServer.Host.Services.GetService(typeof(IBankAccountService)) as IBankAccountService;
			ApiBankAccountServerResponseModel model = await service.Get(1);

			ApiBankAccountClientRequestModel request = mapper.MapServerResponseToClientRequest(model);
			request.SetProperties("B", 1, "B");

			UpdateResponse<ApiBankAccountClientResponseModel> updateResponse = await client.BankAccountUpdateAsync(model.Id, request);

			context.Entry(context.Set<BankAccount>().ToList()[0]).Reload();
			updateResponse.Record.Should().NotBeNull();
			updateResponse.Success.Should().BeTrue();
			updateResponse.Record.Id.Should().Be(1);
			context.Set<BankAccount>().ToList()[0].AccountNumber.Should().Be("B");
			context.Set<BankAccount>().ToList()[0].ArtistId.Should().Be(1);
			context.Set<BankAccount>().ToList()[0].RoutingNumber.Should().Be("B");

			updateResponse.Record.Id.Should().Be(1);
			updateResponse.Record.AccountNumber.Should().Be("B");
			updateResponse.Record.ArtistId.Should().Be(1);
			updateResponse.Record.RoutingNumber.Should().Be("B");
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

			IBankAccountService service = testServer.Host.Services.GetService(typeof(IBankAccountService)) as IBankAccountService;
			var model = new ApiBankAccountServerRequestModel();
			model.SetProperties("B", 1, "B");
			CreateResponse<ApiBankAccountServerResponseModel> createdResponse = await service.Create(model);

			createdResponse.Success.Should().BeTrue();

			ActionResponse deleteResult = await client.BankAccountDeleteAsync(2);

			deleteResult.Success.Should().BeTrue();
			ApiBankAccountServerResponseModel verifyResponse = await service.Get(2);

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

			ApiBankAccountClientResponseModel response = await client.BankAccountGetAsync(1);

			response.Should().NotBeNull();
			response.AccountNumber.Should().Be("A");
			response.ArtistId.Should().Be(1);
			response.Id.Should().Be(1);
			response.RoutingNumber.Should().Be("A");
		}

		[Fact]
		public virtual async void TestGetNotFound()
		{
			var builder = new WebHostBuilder()
			              .UseEnvironment("Production")
			              .UseStartup<TestStartup>();
			TestServer testServer = new TestServer(builder);

			var client = new ApiClient(testServer.CreateClient());
			ApiBankAccountClientResponseModel response = await client.BankAccountGetAsync(default(int));

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

			List<ApiBankAccountClientResponseModel> response = await client.BankAccountAllAsync();

			response.Count.Should().BeGreaterThan(0);
			response[0].AccountNumber.Should().Be("A");
			response[0].ArtistId.Should().Be(1);
			response[0].Id.Should().Be(1);
			response[0].RoutingNumber.Should().Be("A");
		}

		[Fact]
		public virtual async void TestByArtistIdFound()
		{
			var builder = new WebHostBuilder()
			              .UseEnvironment("Production")
			              .UseStartup<TestStartup>();
			TestServer testServer = new TestServer(builder);

			var client = new ApiClient(testServer.CreateClient());
			List<ApiBankAccountClientResponseModel> response = await client.ByBankAccountByArtistId(1);

			response.Should().NotBeEmpty();
			response[0].AccountNumber.Should().Be("A");
			response[0].ArtistId.Should().Be(1);
			response[0].Id.Should().Be(1);
			response[0].RoutingNumber.Should().Be("A");
		}

		[Fact]
		public virtual async void TestByArtistIdNotFound()
		{
			var builder = new WebHostBuilder()
			              .UseEnvironment("Production")
			              .UseStartup<TestStartup>();
			TestServer testServer = new TestServer(builder);

			var client = new ApiClient(testServer.CreateClient());
			List<ApiBankAccountClientResponseModel> response = await client.ByBankAccountByArtistId(default(int));

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
				var result = await client.BankAccountAllAsync(token);
			};

			testCancellation.Should().Throw<OperationCanceledException>();
		}
	}
}

/*<Codenesium>
    <Hash>3a33280a774b16fe10f58fed4f9e3cb9</Hash>
</Codenesium>*/