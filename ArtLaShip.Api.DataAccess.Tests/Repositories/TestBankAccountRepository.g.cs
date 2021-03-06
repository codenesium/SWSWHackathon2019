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
	public partial class BankAccountRepositoryMoc
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

		public static Mock<ILogger<BankAccountRepository>> GetLoggerMoc()
		{
			return new Mock<ILogger<BankAccountRepository>>();
		}
	}

	[Trait("Type", "Unit")]
	[Trait("Table", "BankAccount")]
	[Trait("Area", "Repositories")]
	public partial class BankAccountRepositoryTests
	{
		[Fact]
		public async void All()
		{
			Mock<ILogger<BankAccountRepository>> loggerMoc = BankAccountRepositoryMoc.GetLoggerMoc();
			ApplicationDbContext context = BankAccountRepositoryMoc.GetContext();
			var repository = new BankAccountRepository(loggerMoc.Object, context);
			var records = await repository.All();

			records.Should().NotBeEmpty();
			records.Count.Should().Be(1);
		}

		[Fact]
		public async void AllWithSearch()
		{
			Mock<ILogger<BankAccountRepository>> loggerMoc = BankAccountRepositoryMoc.GetLoggerMoc();
			ApplicationDbContext context = BankAccountRepositoryMoc.GetContext();
			var repository = new BankAccountRepository(loggerMoc.Object, context);
			var records = await repository.All(1, 0, "A".ToString());

			records.Should().NotBeEmpty();
			records.Count.Should().Be(1);
		}

		[Fact]
		public async void Get()
		{
			Mock<ILogger<BankAccountRepository>> loggerMoc = BankAccountRepositoryMoc.GetLoggerMoc();
			ApplicationDbContext context = BankAccountRepositoryMoc.GetContext();
			var repository = new BankAccountRepository(loggerMoc.Object, context);

			BankAccount entity = new BankAccount();
			entity.SetProperties(default(int), "B", 1, "B");
			context.Set<BankAccount>().Add(entity);
			await context.SaveChangesAsync();

			var record = await repository.Get(entity.Id);

			record.Should().NotBeNull();
		}

		[Fact]
		public async void Create()
		{
			Mock<ILogger<BankAccountRepository>> loggerMoc = BankAccountRepositoryMoc.GetLoggerMoc();
			ApplicationDbContext context = BankAccountRepositoryMoc.GetContext();
			var repository = new BankAccountRepository(loggerMoc.Object, context);

			var entity = new BankAccount();
			entity.SetProperties(default(int), "B", 1, "B");
			await repository.Create(entity);

			var records = await context.Set<BankAccount>().Where(x => true).ToListAsync();

			records.Count.Should().Be(2);
		}

		[Fact]
		public async void Update_Entity_Is_Tracked()
		{
			Mock<ILogger<BankAccountRepository>> loggerMoc = BankAccountRepositoryMoc.GetLoggerMoc();
			ApplicationDbContext context = BankAccountRepositoryMoc.GetContext();
			var repository = new BankAccountRepository(loggerMoc.Object, context);
			BankAccount entity = new BankAccount();
			entity.SetProperties(default(int), "B", 1, "B");
			context.Set<BankAccount>().Add(entity);
			await context.SaveChangesAsync();

			var record = await repository.Get(entity.Id);

			await repository.Update(record);

			var records = await context.Set<BankAccount>().Where(x => true).ToListAsync();

			records.Count.Should().Be(2);
		}

		[Fact]
		public async void Update_Entity_Is_Not_Tracked()
		{
			Mock<ILogger<BankAccountRepository>> loggerMoc = BankAccountRepositoryMoc.GetLoggerMoc();
			ApplicationDbContext context = BankAccountRepositoryMoc.GetContext();
			var repository = new BankAccountRepository(loggerMoc.Object, context);
			BankAccount entity = new BankAccount();
			entity.SetProperties(default(int), "B", 1, "B");
			context.Set<BankAccount>().Add(entity);
			await context.SaveChangesAsync();

			context.Entry(entity).State = EntityState.Detached;

			await repository.Update(entity);

			var records = await context.Set<BankAccount>().Where(x => true).ToListAsync();

			records.Count.Should().Be(2);
		}

		[Fact]
		public async void DeleteFound()
		{
			Mock<ILogger<BankAccountRepository>> loggerMoc = BankAccountRepositoryMoc.GetLoggerMoc();
			ApplicationDbContext context = BankAccountRepositoryMoc.GetContext();
			var repository = new BankAccountRepository(loggerMoc.Object, context);
			BankAccount entity = new BankAccount();
			entity.SetProperties(default(int), "B", 1, "B");
			context.Set<BankAccount>().Add(entity);
			await context.SaveChangesAsync();

			await repository.Delete(entity.Id);

			var records = await context.Set<BankAccount>().Where(x => true).ToListAsync();

			records.Count.Should().Be(1);
		}

		[Fact]
		public void DeleteNotFound()
		{
			Mock<ILogger<BankAccountRepository>> loggerMoc = BankAccountRepositoryMoc.GetLoggerMoc();
			ApplicationDbContext context = BankAccountRepositoryMoc.GetContext();
			var repository = new BankAccountRepository(loggerMoc.Object, context);

			Func<Task> delete = async () =>
			{
				await repository.Delete(default(int));
			};

			delete.Should().NotThrow();
		}
	}
}

/*<Codenesium>
    <Hash>cd0b7371ae2c070f53e7303e25078450</Hash>
</Codenesium>*/