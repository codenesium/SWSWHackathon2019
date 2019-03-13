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
	public abstract class AbstractEmailRepository : AbstractRepository
	{
		protected ApplicationDbContext Context { get; }

		protected ILogger Logger { get; }

		public AbstractEmailRepository(
			ILogger logger,
			ApplicationDbContext context)
			: base()
		{
			this.Logger = logger;
			this.Context = context;
		}

		public virtual Task<List<Email>> All(int limit = int.MaxValue, int offset = 0, string query = "")
		{
			if (string.IsNullOrWhiteSpace(query))
			{
				return this.Where(x => true, limit, offset);
			}
			else
			{
				return this.Where(x =>
				                  x.ArtistId == query.ToInt() ||
				                  x.DateCreated == query.ToDateTime() ||
				                  x.Email1.StartsWith(query) ||
				                  x.Id == query.ToInt(),
				                  limit,
				                  offset);
			}
		}

		public async virtual Task<Email> Get(int id)
		{
			return await this.GetById(id);
		}

		public async virtual Task<Email> Create(Email item)
		{
			this.Context.Set<Email>().Add(item);
			await this.Context.SaveChangesAsync();

			this.Context.Entry(item).State = EntityState.Detached;
			return item;
		}

		public async virtual Task Update(Email item)
		{
			var entity = this.Context.Set<Email>().Local.FirstOrDefault(x => x.Id == item.Id);
			if (entity == null)
			{
				this.Context.Set<Email>().Attach(item);
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
			Email record = await this.GetById(id);

			if (record == null)
			{
				return;
			}
			else
			{
				this.Context.Set<Email>().Remove(record);
				await this.Context.SaveChangesAsync();
			}
		}

		// Non-unique constraint IX_Email_artistId.
		public async virtual Task<List<Email>> ByArtistId(int artistId, int limit = int.MaxValue, int offset = 0)
		{
			return await this.Where(x => x.ArtistId == artistId, limit, offset);
		}

		// Foreign key reference to table Artist via artistId.
		public async virtual Task<Artist> ArtistByArtistId(int artistId)
		{
			return await this.Context.Set<Artist>()
			       .SingleOrDefaultAsync(x => x.Id == artistId);
		}

		protected async Task<List<Email>> Where(
			Expression<Func<Email, bool>> predicate,
			int limit = int.MaxValue,
			int offset = 0,
			Expression<Func<Email, dynamic>> orderBy = null)
		{
			if (orderBy == null)
			{
				orderBy = x => x.Id;
			}

			return await this.Context.Set<Email>()
			       .Include(x => x.ArtistIdNavigation)

			       .Where(predicate).AsQueryable().OrderBy(orderBy).Skip(offset).Take(limit).ToListAsync<Email>();
		}

		private async Task<Email> GetById(int id)
		{
			List<Email> records = await this.Where(x => x.Id == id);

			return records.FirstOrDefault();
		}
	}
}

/*<Codenesium>
    <Hash>d5b45b495af7ebfb1a9fdbe0256bf19b</Hash>
</Codenesium>*/