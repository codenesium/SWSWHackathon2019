using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ArtLaShipNS.Api.DataAccess
{
	public partial class EmailRepositoryMoc
	{
		public static ApplicationDbContext GetContext()
		{
			SqliteConnectionStringBuilder connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
			string connectionString = connectionStringBuilder.ToString();
			SqliteConnection connection = new SqliteConnection(connectionString);
			DbContextOptionsBuilder options = new DbContextOptionsBuilder();
			options.UseSqlite(connection);
			var context = new ApplicationDbContext(options.Options);
			context.Database.OpenConnection();
			context.Database.EnsureCreated();
			IntegrationTestMigration migrator = new IntegrationTestMigration(context);
			migrator.Migrate().Wait();
			return context;
		}

		public static Mock<ILogger<EmailRepository>> GetLoggerMoc()
		{
			return new Mock<ILogger<EmailRepository>>();
		}
	}

	[Trait("Type", "Unit")]
	[Trait("Table", "Email")]
	[Trait("Area", "Repositories")]
	public partial class EmailRepositoryTests
	{
		[Fact]
		public async void All()
		{
			Mock<ILogger<EmailRepository>> loggerMoc = EmailRepositoryMoc.GetLoggerMoc();
			ApplicationDbContext context = EmailRepositoryMoc.GetContext();
			var repository = new EmailRepository(loggerMoc.Object, context);
			var records = await repository.All();

			records.Should().NotBeEmpty();
			records.Count.Should().Be(1);
		}

		[Fact]
		public async void AllWithSearch()
		{
			Mock<ILogger<EmailRepository>> loggerMoc = EmailRepositoryMoc.GetLoggerMoc();
			ApplicationDbContext context = EmailRepositoryMoc.GetContext();
			var repository = new EmailRepository(loggerMoc.Object, context);
			var records = await repository.All(1, 0, 1.ToString());

			records.Should().NotBeEmpty();
			records.Count.Should().Be(1);
		}

		[Fact]
		public async void Get()
		{
			Mock<ILogger<EmailRepository>> loggerMoc = EmailRepositoryMoc.GetLoggerMoc();
			ApplicationDbContext context = EmailRepositoryMoc.GetContext();
			var repository = new EmailRepository(loggerMoc.Object, context);

			Email entity = new Email();
			entity.SetProperties(default(int), 1, DateTime.Parse("1/1/1988 12:00:00 AM"), "B");
			context.Set<Email>().Add(entity);
			await context.SaveChangesAsync();

			var record = await repository.Get(entity.Id);

			record.Should().NotBeNull();
		}

		[Fact]
		public async void Create()
		{
			Mock<ILogger<EmailRepository>> loggerMoc = EmailRepositoryMoc.GetLoggerMoc();
			ApplicationDbContext context = EmailRepositoryMoc.GetContext();
			var repository = new EmailRepository(loggerMoc.Object, context);

			var entity = new Email();
			entity.SetProperties(default(int), 1, DateTime.Parse("1/1/1988 12:00:00 AM"), "B");
			await repository.Create(entity);

			var records = await context.Set<Email>().Where(x => true).ToListAsync();

			records.Count.Should().Be(2);
		}

		[Fact]
		public async void Update_Entity_Is_Tracked()
		{
			Mock<ILogger<EmailRepository>> loggerMoc = EmailRepositoryMoc.GetLoggerMoc();
			ApplicationDbContext context = EmailRepositoryMoc.GetContext();
			var repository = new EmailRepository(loggerMoc.Object, context);
			Email entity = new Email();
			entity.SetProperties(default(int), 1, DateTime.Parse("1/1/1988 12:00:00 AM"), "B");
			context.Set<Email>().Add(entity);
			await context.SaveChangesAsync();

			var record = await repository.Get(entity.Id);

			await repository.Update(record);

			var records = await context.Set<Email>().Where(x => true).ToListAsync();

			records.Count.Should().Be(2);
		}

		[Fact]
		public async void Update_Entity_Is_Not_Tracked()
		{
			Mock<ILogger<EmailRepository>> loggerMoc = EmailRepositoryMoc.GetLoggerMoc();
			ApplicationDbContext context = EmailRepositoryMoc.GetContext();
			var repository = new EmailRepository(loggerMoc.Object, context);
			Email entity = new Email();
			entity.SetProperties(default(int), 1, DateTime.Parse("1/1/1988 12:00:00 AM"), "B");
			context.Set<Email>().Add(entity);
			await context.SaveChangesAsync();

			context.Entry(entity).State = EntityState.Detached;

			await repository.Update(entity);

			var records = await context.Set<Email>().Where(x => true).ToListAsync();

			records.Count.Should().Be(2);
		}

		[Fact]
		public async void DeleteFound()
		{
			Mock<ILogger<EmailRepository>> loggerMoc = EmailRepositoryMoc.GetLoggerMoc();
			ApplicationDbContext context = EmailRepositoryMoc.GetContext();
			var repository = new EmailRepository(loggerMoc.Object, context);
			Email entity = new Email();
			entity.SetProperties(default(int), 1, DateTime.Parse("1/1/1988 12:00:00 AM"), "B");
			context.Set<Email>().Add(entity);
			await context.SaveChangesAsync();

			await repository.Delete(entity.Id);

			var records = await context.Set<Email>().Where(x => true).ToListAsync();

			records.Count.Should().Be(1);
		}

		[Fact]
		public void DeleteNotFound()
		{
			Mock<ILogger<EmailRepository>> loggerMoc = EmailRepositoryMoc.GetLoggerMoc();
			ApplicationDbContext context = EmailRepositoryMoc.GetContext();
			var repository = new EmailRepository(loggerMoc.Object, context);

			Func<Task> delete = async () =>
			{
				await repository.Delete(default(int));
			};

			delete.Should().NotThrow();
		}
	}
}

/*<Codenesium>
    <Hash>249ccf9d07a043c0115666527991c99c</Hash>
</Codenesium>*/