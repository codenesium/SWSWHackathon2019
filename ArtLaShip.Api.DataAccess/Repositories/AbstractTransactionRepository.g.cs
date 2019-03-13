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
	public abstract class AbstractTransactionRepository : AbstractRepository
	{
		protected ApplicationDbContext Context { get; }

		protected ILogger Logger { get; }

		public AbstractTransactionRepository(
			ILogger logger,
			ApplicationDbContext context)
			: base()
		{
			this.Logger = logger;
			this.Context = context;
		}

		public virtual Task<List<Transaction>> All(int limit = int.MaxValue, int offset = 0, string query = "")
		{
			if (string.IsNullOrWhiteSpace(query))
			{
				return this.Where(x => true, limit, offset);
			}
			else
			{
				return this.Where(x =>
				                  x.Amount.ToDecimal() == query.ToDecimal() ||
				                  x.ArtistId == query.ToInt() ||
				                  x.DateCreated == query.ToDateTime() ||
				                  x.Id == query.ToInt() ||
				                  x.StripeTransactionId.StartsWith(query),
				                  limit,
				                  offset);
			}
		}

		public async virtual Task<Transaction> Get(int id)
		{
			return await this.GetById(id);
		}

		public async virtual Task<Transaction> Create(Transaction item)
		{
			this.Context.Set<Transaction>().Add(item);
			await this.Context.SaveChangesAsync();

			this.Context.Entry(item).State = EntityState.Detached;
			return item;
		}

		public async virtual Task Update(Transaction item)
		{
			var entity = this.Context.Set<Transaction>().Local.FirstOrDefault(x => x.Id == item.Id);
			if (entity == null)
			{
				this.Context.Set<Transaction>().Attach(item);
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
			Transaction record = await this.GetById(id);

			if (record == null)
			{
				return;
			}
			else
			{
				this.Context.Set<Transaction>().Remove(record);
				await this.Context.SaveChangesAsync();
			}
		}

		// Non-unique constraint IX_Transaction_artistId.
		public async virtual Task<List<Transaction>> ByArtistId(int artistId, int limit = int.MaxValue, int offset = 0)
		{
			return await this.Where(x => x.ArtistId == artistId, limit, offset);
		}

		// Foreign key reference to table Artist via artistId.
		public async virtual Task<Artist> ArtistByArtistId(int artistId)
		{
			return await this.Context.Set<Artist>()
			       .SingleOrDefaultAsync(x => x.Id == artistId);
		}

		protected async Task<List<Transaction>> Where(
			Expression<Func<Transaction, bool>> predicate,
			int limit = int.MaxValue,
			int offset = 0,
			Expression<Func<Transaction, dynamic>> orderBy = null)
		{
			if (orderBy == null)
			{
				orderBy = x => x.Id;
			}

			return await this.Context.Set<Transaction>()
			       .Include(x => x.ArtistIdNavigation)

			       .Where(predicate).AsQueryable().OrderBy(orderBy).Skip(offset).Take(limit).ToListAsync<Transaction>();
		}

		private async Task<Transaction> GetById(int id)
		{
			List<Transaction> records = await this.Where(x => x.Id == id);

			return records.FirstOrDefault();
		}
	}
}

/*<Codenesium>
    <Hash>338bd962e2e894bf100eea039d961c33</Hash>
</Codenesium>*/