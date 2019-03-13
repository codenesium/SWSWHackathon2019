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
	public partial class ArtistRepositoryMoc
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

		public static Mock<ILogger<ArtistRepository>> GetLoggerMoc()
		{
			return new Mock<ILogger<ArtistRepository>>();
		}
	}

	[Trait("Type", "Unit")]
	[Trait("Table", "Artist")]
	[Trait("Area", "Repositories")]
	public partial class ArtistRepositoryTests
	{
		[Fact]
		public async void All()
		{
			Mock<ILogger<ArtistRepository>> loggerMoc = ArtistRepositoryMoc.GetLoggerMoc();
			ApplicationDbContext context = ArtistRepositoryMoc.GetContext();
			var repository = new ArtistRepository(loggerMoc.Object, context);
			var records = await repository.All();

			records.Should().NotBeEmpty();
			records.Count.Should().Be(1);
		}

		[Fact]
		public async void AllWithSearch()
		{
			Mock<ILogger<ArtistRepository>> loggerMoc = ArtistRepositoryMoc.GetLoggerMoc();
			ApplicationDbContext context = ArtistRepositoryMoc.GetContext();
			var repository = new ArtistRepository(loggerMoc.Object, context);
			var records = await repository.All(1, 0, "A".ToString());

			records.Should().NotBeEmpty();
			records.Count.Should().Be(1);
		}

		[Fact]
		public async void Get()
		{
			Mock<ILogger<ArtistRepository>> loggerMoc = ArtistRepositoryMoc.GetLoggerMoc();
			ApplicationDbContext context = ArtistRepositoryMoc.GetContext();
			var repository = new ArtistRepository(loggerMoc.Object, context);

			Artist entity = new Artist();
			entity.SetProperties(default(int), "B", "B", Guid.Parse("3842cac4-b9a0-8223-0dcc-509a6f75849b"), "B", "B", "B", "B", "B");
			context.Set<Artist>().Add(entity);
			await context.SaveChangesAsync();

			var record = await repository.Get(entity.Id);

			record.Should().NotBeNull();
		}

		[Fact]
		public async void Create()
		{
			Mock<ILogger<ArtistRepository>> loggerMoc = ArtistRepositoryMoc.GetLoggerMoc();
			ApplicationDbContext context = ArtistRepositoryMoc.GetContext();
			var repository = new ArtistRepository(loggerMoc.Object, context);

			var entity = new Artist();
			entity.SetProperties(default(int), "B", "B", Guid.Parse("3842cac4-b9a0-8223-0dcc-509a6f75849b"), "B", "B", "B", "B", "B");
			await repository.Create(entity);

			var records = await context.Set<Artist>().Where(x => true).ToListAsync();

			records.Count.Should().Be(2);
		}

		[Fact]
		public async void Update_Entity_Is_Tracked()
		{
			Mock<ILogger<ArtistRepository>> loggerMoc = ArtistRepositoryMoc.GetLoggerMoc();
			ApplicationDbContext context = ArtistRepositoryMoc.GetContext();
			var repository = new ArtistRepository(loggerMoc.Object, context);
			Artist entity = new Artist();
			entity.SetProperties(default(int), "B", "B", Guid.Parse("3842cac4-b9a0-8223-0dcc-509a6f75849b"), "B", "B", "B", "B", "B");
			context.Set<Artist>().Add(entity);
			await context.SaveChangesAsync();

			var record = await repository.Get(entity.Id);

			await repository.Update(record);

			var records = await context.Set<Artist>().Where(x => true).ToListAsync();

			records.Count.Should().Be(2);
		}

		[Fact]
		public async void Update_Entity_Is_Not_Tracked()
		{
			Mock<ILogger<ArtistRepository>> loggerMoc = ArtistRepositoryMoc.GetLoggerMoc();
			ApplicationDbContext context = ArtistRepositoryMoc.GetContext();
			var repository = new ArtistRepository(loggerMoc.Object, context);
			Artist entity = new Artist();
			entity.SetProperties(default(int), "B", "B", Guid.Parse("3842cac4-b9a0-8223-0dcc-509a6f75849b"), "B", "B", "B", "B", "B");
			context.Set<Artist>().Add(entity);
			await context.SaveChangesAsync();

			context.Entry(entity).State = EntityState.Detached;

			await repository.Update(entity);

			var records = await context.Set<Artist>().Where(x => true).ToListAsync();

			records.Count.Should().Be(2);
		}

		[Fact]
		public async void DeleteFound()
		{
			Mock<ILogger<ArtistRepository>> loggerMoc = ArtistRepositoryMoc.GetLoggerMoc();
			ApplicationDbContext context = ArtistRepositoryMoc.GetContext();
			var repository = new ArtistRepository(loggerMoc.Object, context);
			Artist entity = new Artist();
			entity.SetProperties(default(int), "B", "B", Guid.Parse("3842cac4-b9a0-8223-0dcc-509a6f75849b"), "B", "B", "B", "B", "B");
			context.Set<Artist>().Add(entity);
			await context.SaveChangesAsync();

			await repository.Delete(entity.Id);

			var records = await context.Set<Artist>().Where(x => true).ToListAsync();

			records.Count.Should().Be(1);
		}

		[Fact]
		public void DeleteNotFound()
		{
			Mock<ILogger<ArtistRepository>> loggerMoc = ArtistRepositoryMoc.GetLoggerMoc();
			ApplicationDbContext context = ArtistRepositoryMoc.GetContext();
			var repository = new ArtistRepository(loggerMoc.Object, context);

			Func<Task> delete = async () =>
			{
				await repository.Delete(default(int));
			};

			delete.Should().NotThrow();
		}
	}
}

/*<Codenesium>
    <Hash>e9acc6fa64b75fab7a373682b3f590e1</Hash>
</Codenesium>*/