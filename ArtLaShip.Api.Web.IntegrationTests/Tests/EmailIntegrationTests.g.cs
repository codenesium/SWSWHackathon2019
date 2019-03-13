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
	[Trait("Table", "Email")]
	[Trait("Area", "Integration")]
	public partial class EmailIntegrationTests
	{
		public EmailIntegrationTests()
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

			var model = new ApiEmailClientRequestModel();
			model.SetProperties(1, DateTime.Parse("1/1/1988 12:00:00 AM"), "B");
			var model2 = new ApiEmailClientRequestModel();
			model2.SetProperties(1, DateTime.Parse("1/1/1989 12:00:00 AM"), "C");
			var request = new List<ApiEmailClientRequestModel>() {model, model2};
			CreateResponse<List<ApiEmailClientResponseModel>> result = await client.EmailBulkInsertAsync(request);

			result.Success.Should().BeTrue();
			result.Record.Should().NotBeNull();

			context.Set<Email>().ToList()[1].ArtistId.Should().Be(1);
			context.Set<Email>().ToList()[1].DateCreated.Should().Be(DateTime.Parse("1/1/1988 12:00:00 AM"));
			context.Set<Email>().ToList()[1].EmailValue.Should().Be("B");

			context.Set<Email>().ToList()[2].ArtistId.Should().Be(1);
			context.Set<Email>().ToList()[2].DateCreated.Should().Be(DateTime.Parse("1/1/1989 12:00:00 AM"));
			context.Set<Email>().ToList()[2].EmailValue.Should().Be("C");
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

			var model = new ApiEmailClientRequestModel();
			model.SetProperties(1, DateTime.Parse("1/1/1988 12:00:00 AM"), "B");
			CreateResponse<ApiEmailClientResponseModel> result = await client.EmailCreateAsync(model);

			result.Success.Should().BeTrue();
			result.Record.Should().NotBeNull();
			context.Set<Email>().ToList()[1].ArtistId.Should().Be(1);
			context.Set<Email>().ToList()[1].DateCreated.Should().Be(DateTime.Parse("1/1/1988 12:00:00 AM"));
			context.Set<Email>().ToList()[1].EmailValue.Should().Be("B");

			result.Record.ArtistId.Should().Be(1);
			result.Record.DateCreated.Should().Be(DateTime.Parse("1/1/1988 12:00:00 AM"));
			result.Record.EmailValue.Should().Be("B");
		}

		[Fact]
		public virtual async void TestUpdate()
		{
			var builder = new WebHostBuilder()
			              .UseEnvironment("Production")
			              .UseStartup<TestStartup>();
			TestServer testServer = new TestServer(builder);

			var client = new ApiClient(testServer.CreateClient());
			var mapper = new ApiEmailServerModelMapper();
			ApplicationDbContext context = testServer.Host.Services.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
			IEmailService service = testServer.Host.Services.GetService(typeof(IEmailService)) as IEmailService;
			ApiEmailServerResponseModel model = await service.Get(1);

			ApiEmailClientRequestModel request = mapper.MapServerResponseToClientRequest(model);
			request.SetProperties(1, DateTime.Parse("1/1/1988 12:00:00 AM"), "B");

			UpdateResponse<ApiEmailClientResponseModel> updateResponse = await client.EmailUpdateAsync(model.Id, request);

			context.Entry(context.Set<Email>().ToList()[0]).Reload();
			updateResponse.Record.Should().NotBeNull();
			updateResponse.Success.Should().BeTrue();
			updateResponse.Record.Id.Should().Be(1);
			context.Set<Email>().ToList()[0].ArtistId.Should().Be(1);
			context.Set<Email>().ToList()[0].DateCreated.Should().Be(DateTime.Parse("1/1/1988 12:00:00 AM"));
			context.Set<Email>().ToList()[0].EmailValue.Should().Be("B");

			updateResponse.Record.Id.Should().Be(1);
			updateResponse.Record.ArtistId.Should().Be(1);
			updateResponse.Record.DateCreated.Should().Be(DateTime.Parse("1/1/1988 12:00:00 AM"));
			updateResponse.Record.EmailValue.Should().Be("B");
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

			IEmailService service = testServer.Host.Services.GetService(typeof(IEmailService)) as IEmailService;
			var model = new ApiEmailServerRequestModel();
			model.SetProperties(1, DateTime.Parse("1/1/1988 12:00:00 AM"), "B");
			CreateResponse<ApiEmailServerResponseModel> createdResponse = await service.Create(model);

			createdResponse.Success.Should().BeTrue();

			ActionResponse deleteResult = await client.EmailDeleteAsync(2);

			deleteResult.Success.Should().BeTrue();
			ApiEmailServerResponseModel verifyResponse = await service.Get(2);

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

			ApiEmailClientResponseModel response = await client.EmailGetAsync(1);

			response.Should().NotBeNull();
			response.ArtistId.Should().Be(1);
			response.DateCreated.Should().Be(DateTime.Parse("1/1/1987 12:00:00 AM"));
			response.EmailValue.Should().Be("A");
			response.Id.Should().Be(1);
		}

		[Fact]
		public virtual async void TestGetNotFound()
		{
			var builder = new WebHostBuilder()
			              .UseEnvironment("Production")
			              .UseStartup<TestStartup>();
			TestServer testServer = new TestServer(builder);

			var client = new ApiClient(testServer.CreateClient());
			ApiEmailClientResponseModel response = await client.EmailGetAsync(default(int));

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

			List<ApiEmailClientResponseModel> response = await client.EmailAllAsync();

			response.Count.Should().BeGreaterThan(0);
			response[0].ArtistId.Should().Be(1);
			response[0].DateCreated.Should().Be(DateTime.Parse("1/1/1987 12:00:00 AM"));
			response[0].EmailValue.Should().Be("A");
			response[0].Id.Should().Be(1);
		}

		[Fact]
		public virtual async void TestByArtistIdFound()
		{
			var builder = new WebHostBuilder()
			              .UseEnvironment("Production")
			              .UseStartup<TestStartup>();
			TestServer testServer = new TestServer(builder);

			var client = new ApiClient(testServer.CreateClient());
			List<ApiEmailClientResponseModel> response = await client.ByEmailByArtistId(1);

			response.Should().NotBeEmpty();
			response[0].ArtistId.Should().Be(1);
			response[0].DateCreated.Should().Be(DateTime.Parse("1/1/1987 12:00:00 AM"));
			response[0].EmailValue.Should().Be("A");
			response[0].Id.Should().Be(1);
		}

		[Fact]
		public virtual async void TestByArtistIdNotFound()
		{
			var builder = new WebHostBuilder()
			              .UseEnvironment("Production")
			              .UseStartup<TestStartup>();
			TestServer testServer = new TestServer(builder);

			var client = new ApiClient(testServer.CreateClient());
			List<ApiEmailClientResponseModel> response = await client.ByEmailByArtistId(default(int));

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
				var result = await client.EmailAllAsync(token);
			};

			testCancellation.Should().Throw<OperationCanceledException>();
		}
	}
}

/*<Codenesium>
    <Hash>d19cbc2f8a08af68f91452f6a8c88181</Hash>
</Codenesium>*/