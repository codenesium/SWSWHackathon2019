using Codenesium.DataConversionExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.DataAccess
{
	public abstract class AbstractArtistRepository : AbstractRepository
	{
		protected ApplicationDbContext Context { get; }

		protected ILogger Logger { get; }

		public AbstractArtistRepository(
			ILogger logger,
			ApplicationDbContext context)
			: base()
		{
			this.Logger = logger;
			this.Context = context;
		}

		public virtual Task<List<Artist>> All(int limit = int.MaxValue, int offset = 0, string query = "")
		{
			if (string.IsNullOrWhiteSpace(query))
			{
				return this.Where(x => true, limit, offset);
			}
			else
			{
				return this.Where(x =>
				                  x.AspNetUserId.StartsWith(query) ||
				                  x.Bio.StartsWith(query) ||
				                  x.ExternalId == query.ToNullableGuid() ||
				                  x.Facebook.StartsWith(query) ||
				                  x.Id == query.ToInt() ||
				                  x.Name.StartsWith(query) ||
				                  x.SoundCloud.StartsWith(query) ||
				                  x.Twitter.StartsWith(query) ||
				                  x.Website.StartsWith(query),
				                  limit,
				                  offset);
			}
		}

		public async virtual Task<Artist> Get(int id)
		{
			return await this.GetById(id);
		}

		public async virtual Task<Artist> Create(Artist item)
		{
			this.Context.Set<Artist>().Add(item);
			await this.Context.SaveChangesAsync();

			this.Context.Entry(item).State = EntityState.Detached;
			return item;
		}

		public async virtual Task Update(Artist item)
		{
			var entity = this.Context.Set<Artist>().Local.FirstOrDefault(x => x.Id == item.Id);
			if (entity == null)
			{
				this.Context.Set<Artist>().Attach(item);
			}
			else
			{
				this.Context.Entry(entity).CurrentValues.SetValues(item);
			}

			await this.Context.SaveChangesAsync();
		}

		public async virtual Task Delete(
			int id)
		{
			Artist record = await this.GetById(id);

			if (record == null)
			{
				return;
			}
			else
			{
				this.Context.Set<Artist>().Remove(record);
				await this.Context.SaveChangesAsync();
			}
		}

		// Foreign key reference to this table BankAccount via artistId.
		public async virtual Task<List<BankAccount>> BankAccountsByArtistId(int artistId, int limit = int.MaxValue, int offset = 0)
		{
			return await this.Context.Set<BankAccount>()
			       .Include(x => x.ArtistIdNavigation)
			       .Where(x => x.ArtistId == artistId).AsQueryable().Skip(offset).Take(limit).ToListAsync<BankAccount>();
		}

		// Foreign key reference to this table Email via artistId.
		public async virtual Task<List<Email>> EmailsByArtistId(int artistId, int limit = int.MaxValue, int offset = 0)
		{
			return await this.Context.Set<Email>()
			       .Include(x => x.ArtistIdNavigation)
			       .Where(x => x.ArtistId == artistId).AsQueryable().Skip(offset).Take(limit).ToListAsync<Email>();
		}

		// Foreign key reference to this table Transaction via artistId.
		public async virtual Task<List<Transaction>> TransactionsByArtistId(int artistId, int limit = int.MaxValue, int offset = 0)
		{
			return await this.Context.Set<Transaction>()
			       .Include(x => x.ArtistIdNavigation)
			       .Where(x => x.ArtistId == artistId).AsQueryable().Skip(offset).Take(limit).ToListAsync<Transaction>();
		}

		protected async Task<List<Artist>> Where(
			Expression<Func<Artist, bool>> predicate,
			int limit = int.MaxValue,
			int offset = 0,
			Expression<Func<Artist, dynamic>> orderBy = null)
		{
			if (orderBy == null)
			{
				orderBy = x => x.Id;
			}

			return await this.Context.Set<Artist>()

			       .Where(predicate).AsQueryable().OrderBy(orderBy).Skip(offset).Take(limit).ToListAsync<Artist>();
		}

		private async Task<Artist> GetById(int id)
		{
			List<Artist> records = await this.Where(x => x.Id == id);

			return records.FirstOrDefault();
		}
	}
}

/*<Codenesium>
    <Hash>02ccd66a12c998e312eb41507d0e336b</Hash>
</Codenesium>*/