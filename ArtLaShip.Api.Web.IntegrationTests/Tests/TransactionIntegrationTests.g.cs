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
	[Trait("Table", "Transaction")]
	[Trait("Area", "Integration")]
	public partial class TransactionIntegrationTests
	{
		public TransactionIntegrationTests()
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

			var model = new ApiTransactionClientRequestModel();
			model.SetProperties(2m, 1, DateTime.Parse("1/1/1988 12:00:00 AM"), "B");
			var model2 = new ApiTransactionClientRequestModel();
			model2.SetProperties(3m, 1, DateTime.Parse("1/1/1989 12:00:00 AM"), "C");
			var request = new List<ApiTransactionClientRequestModel>() {model, model2};
			CreateResponse<List<ApiTransactionClientResponseModel>> result = await client.TransactionBulkInsertAsync(request);

			result.Success.Should().BeTrue();
			result.Record.Should().NotBeNull();

			context.Set<Transaction>().ToList()[1].Amount.Should().Be(2m);
			context.Set<Transaction>().ToList()[1].ArtistId.Should().Be(1);
			context.Set<Transaction>().ToList()[1].DateCreated.Should().Be(DateTime.Parse("1/1/1988 12:00:00 AM"));
			context.Set<Transaction>().ToList()[1].StripeTransactionId.Should().Be("B");

			context.Set<Transaction>().ToList()[2].Amount.Should().Be(3m);
			context.Set<Transaction>().ToList()[2].ArtistId.Should().Be(1);
			context.Set<Transaction>().ToList()[2].DateCreated.Should().Be(DateTime.Parse("1/1/1989 12:00:00 AM"));
			context.Set<Transaction>().ToList()[2].StripeTransactionId.Should().Be("C");
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

			var model = new ApiTransactionClientRequestModel();
			model.SetProperties(2m, 1, DateTime.Parse("1/1/1988 12:00:00 AM"), "B");
			CreateResponse<ApiTransactionClientResponseModel> result = await client.TransactionCreateAsync(model);

			result.Success.Should().BeTrue();
			result.Record.Should().NotBeNull();
			context.Set<Transaction>().ToList()[1].Amount.Should().Be(2m);
			context.Set<Transaction>().ToList()[1].ArtistId.Should().Be(1);
			context.Set<Transaction>().ToList()[1].DateCreated.Should().Be(DateTime.Parse("1/1/1988 12:00:00 AM"));
			context.Set<Transaction>().ToList()[1].StripeTransactionId.Should().Be("B");

			result.Record.Amount.Should().Be(2m);
			result.Record.ArtistId.Should().Be(1);
			result.Record.DateCreated.Should().Be(DateTime.Parse("1/1/1988 12:00:00 AM"));
			result.Record.StripeTransactionId.Should().Be("B");
		}

		[Fact]
		public virtual async void TestUpdate()
		{
			var builder = new WebHostBuilder()
			              .UseEnvironment("Production")
			              .UseStartup<TestStartup>();
			TestServer testServer = new TestServer(builder);

			var client = new ApiClient(testServer.CreateClient());
			var mapper = new ApiTransactionServerModelMapper();
			ApplicationDbContext context = testServer.Host.Services.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
			ITransactionService service = testServer.Host.Services.GetService(typeof(ITransactionService)) as ITransactionService;
			ApiTransactionServerResponseModel model = await service.Get(1);

			ApiTransactionClientRequestModel request = mapper.MapServerResponseToClientRequest(model);
			request.SetProperties(2m, 1, DateTime.Parse("1/1/1988 12:00:00 AM"), "B");

			UpdateResponse<ApiTransactionClientResponseModel> updateResponse = await client.TransactionUpdateAsync(model.Id, request);

			context.Entry(context.Set<Transaction>().ToList()[0]).Reload();
			updateResponse.Record.Should().NotBeNull();
			updateResponse.Success.Should().BeTrue();
			updateResponse.Record.Id.Should().Be(1);
			context.Set<Transaction>().ToList()[0].Amount.Should().Be(2m);
			context.Set<Transaction>().ToList()[0].ArtistId.Should().Be(1);
			context.Set<Transaction>().ToList()[0].DateCreated.Should().Be(DateTime.Parse("1/1/1988 12:00:00 AM"));
			context.Set<Transaction>().ToList()[0].StripeTransactionId.Should().Be("B");

			updateResponse.Record.Id.Should().Be(1);
			updateResponse.Record.Amount.Should().Be(2m);
			updateResponse.Record.ArtistId.Should().Be(1);
			updateResponse.Record.DateCreated.Should().Be(DateTime.Parse("1/1/1988 12:00:00 AM"));
			updateResponse.Record.StripeTransactionId.Should().Be("B");
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

			ITransactionService service = testServer.Host.Services.GetService(typeof(ITransactionService)) as ITransactionService;
			var model = new ApiTransactionServerRequestModel();
			model.SetProperties(2m, 1, DateTime.Parse("1/1/1988 12:00:00 AM"), "B");
			CreateResponse<ApiTransactionServerResponseModel> createdResponse = await service.Create(model);

			createdResponse.Success.Should().BeTrue();

			ActionResponse deleteResult = await client.TransactionDeleteAsync(2);

			deleteResult.Success.Should().BeTrue();
			ApiTransactionServerResponseModel verifyResponse = await service.Get(2);

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

			ApiTransactionClientResponseModel response = await client.TransactionGetAsync(1);

			response.Should().NotBeNull();
			response.Amount.Should().Be(1m);
			response.ArtistId.Should().Be(1);
			response.DateCreated.Should().Be(DateTime.Parse("1/1/1987 12:00:00 AM"));
			response.Id.Should().Be(1);
			response.StripeTransactionId.Should().Be("A");
		}

		[Fact]
		public virtual async void TestGetNotFound()
		{
			var builder = new WebHostBuilder()
			              .UseEnvironment("Production")
			              .UseStartup<TestStartup>();
			TestServer testServer = new TestServer(builder);

			var client = new ApiClient(testServer.CreateClient());
			ApiTransactionClientResponseModel response = await client.TransactionGetAsync(default(int));

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

			List<ApiTransactionClientResponseModel> response = await client.TransactionAllAsync();

			response.Count.Should().BeGreaterThan(0);
			response[0].Amount.Should().Be(1m);
			response[0].ArtistId.Should().Be(1);
			response[0].DateCreated.Should().Be(DateTime.Parse("1/1/1987 12:00:00 AM"));
			response[0].Id.Should().Be(1);
			response[0].StripeTransactionId.Should().Be("A");
		}

		[Fact]
		public virtual async void TestByArtistIdFound()
		{
			var builder = new WebHostBuilder()
			              .UseEnvironment("Production")
			              .UseStartup<TestStartup>();
			TestServer testServer = new TestServer(builder);

			var client = new ApiClient(testServer.CreateClient());
			List<ApiTransactionClientResponseModel> response = await client.ByTransactionByArtistId(1);

			response.Should().NotBeEmpty();
			response[0].Amount.Should().Be(1m);
			response[0].ArtistId.Should().Be(1);
			response[0].DateCreated.Should().Be(DateTime.Parse("1/1/1987 12:00:00 AM"));
			response[0].Id.Should().Be(1);
			response[0].StripeTransactionId.Should().Be("A");
		}

		[Fact]
		public virtual async void TestByArtistIdNotFound()
		{
			var builder = new WebHostBuilder()
			              .UseEnvironment("Production")
			              .UseStartup<TestStartup>();
			TestServer testServer = new TestServer(builder);

			var client = new ApiClient(testServer.CreateClient());
			List<ApiTransactionClientResponseModel> response = await client.ByTransactionByArtistId(default(int));

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
				var result = await client.TransactionAllAsync(token);
			};

			testCancellation.Should().Throw<OperationCanceledException>();
		}
	}
}

/*<Codenesium>
    <Hash>b7d1b59c1a0efef3a64a6015d705be67</Hash>
</Codenesium>*/