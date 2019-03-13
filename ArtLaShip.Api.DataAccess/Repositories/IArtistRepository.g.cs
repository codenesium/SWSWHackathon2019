using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.DataAccess
{
	public partial interface IArtistRepository
	{
		Task<Artist> Create(Artist item);

		Task Update(Artist item);

		Task Delete(int id);

		Task<Artist> Get(int id);

		Task<List<Artist>> All(int limit = int.MaxValue, int offset = 0, string query = "");

		Task<List<BankAccount>> BankAccountsByArtistId(int artistId, int limit = int.MaxValue, int offset = 0);

		Task<List<Email>> EmailsByArtistId(int artistId, int limit = int.MaxValue, int offset = 0);

		Task<List<Transaction>> TransactionsByArtistId(int artistId, int limit = int.MaxValue, int offset = 0);
	}
}

/*<Codenesium>
    <Hash>b0dcb70fe62e8021a23c65a5008776c8</Hash>
</Codenesium>*/