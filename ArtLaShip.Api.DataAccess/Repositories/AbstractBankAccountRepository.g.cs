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
	public abstract class AbstractBankAccountRepository : AbstractRepository
	{
		protected ApplicationDbContext Context { get; }

		protected ILogger Logger { get; }

		public AbstractBankAccountRepository(
			ILogger logger,
			ApplicationDbContext context)
			: base()
		{
			this.Logger = logger;
			this.Context = context;
		}

		public virtual Task<List<BankAccount>> All(int limit = int.MaxValue, int offset = 0, string query = "")
		{
			if (string.IsNullOrWhiteSpace(query))
			{
				return this.Where(x => true, limit, offset);
			}
			else
			{
				return this.Where(x =>
				                  x.AccountNumber.StartsWith(query) ||
				                  x.ArtistId == query.ToInt() ||
				                  x.Id == query.ToInt() ||
				                  x.RoutingNumber.StartsWith(query),
				                  limit,
				                  offset);
			}
		}

		public async virtual Task<BankAccount> Get(int id)
		{
			return await this.GetById(id);
		}

		public async virtual Task<BankAccount> Create(BankAccount item)
		{
			this.Context.Set<BankAccount>().Add(item);
			await this.Context.SaveChangesAsync();

			this.Context.Entry(item).State = EntityState.Detached;
			return item;
		}

		public async virtual Task Update(BankAccount item)
		{
			var entity = this.Context.Set<BankAccount>().Local.FirstOrDefault(x => x.Id == item.Id);
			if (entity == null)
			{
				this.Context.Set<BankAccount>().Attach(item);
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
			BankAccount record = await this.GetById(id);

			if (record == null)
			{
				return;
			}
			else
			{
				this.Context.Set<BankAccount>().Remove(record);
				await this.Context.SaveChangesAsync();
			}
		}

		// Non-unique constraint IX_BankAccount_artistId.
		public async virtual Task<List<BankAccount>> ByArtistId(int artistId, int limit = int.MaxValue, int offset = 0)
		{
			return await this.Where(x => x.ArtistId == artistId, limit, offset);
		}

		// Foreign key reference to table Artist via artistId.
		public async virtual Task<Artist> ArtistByArtistId(int artistId)
		{
			return await this.Context.Set<Artist>()
			       .SingleOrDefaultAsync(x => x.Id == artistId);
		}

		protected async Task<List<BankAccount>> Where(
			Expression<Func<BankAccount, bool>> predicate,
			int limit = int.MaxValue,
			int offset = 0,
			Expression<Func<BankAccount, dynamic>> orderBy = null)
		{
			if (orderBy == null)
			{
				orderBy = x => x.Id;
			}

			return await this.Context.Set<BankAccount>()
			       .Include(x => x.ArtistIdNavigation)

			       .Where(predicate).AsQueryable().OrderBy(orderBy).Skip(offset).Take(limit).ToListAsync<BankAccount>();
		}

		private async Task<BankAccount> GetById(int id)
		{
			List<BankAccount> records = await this.Where(x => x.Id == id);

			return records.FirstOrDefault();
		}
	}
}

/*<Codenesium>
    <Hash>8d11860d8a8ae4f7a838db8cb41ff6fd</Hash>
</Codenesium>*/