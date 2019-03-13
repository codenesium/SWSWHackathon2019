using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.DataAccess
{
	public partial interface ITransactionRepository
	{
		Task<Transaction> Create(Transaction item);

		Task Update(Transaction item);

		Task Delete(int id);

		Task<Transaction> Get(int id);

		Task<List<Transaction>> All(int limit = int.MaxValue, int offset = 0, string query = "");

		Task<List<Transaction>> ByArtistId(int artistId, int limit = int.MaxValue, int offset = 0);

		Task<Artist> ArtistByArtistId(int artistId);
	}
}

/*<Codenesium>
    <Hash>d1bb8773d21e007992563bf2a66519a6</Hash>
</Codenesium>*/