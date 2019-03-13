using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.DataAccess
{
	public partial interface IBankAccountRepository
	{
		Task<BankAccount> Create(BankAccount item);

		Task Update(BankAccount item);

		Task Delete(int id);

		Task<BankAccount> Get(int id);

		Task<List<BankAccount>> All(int limit = int.MaxValue, int offset = 0, string query = "");

		Task<List<BankAccount>> ByArtistId(int artistId, int limit = int.MaxValue, int offset = 0);

		Task<Artist> ArtistByArtistId(int artistId);
	}
}

/*<Codenesium>
    <Hash>62cf64bd4ddec566d6c07d825d36d0ad</Hash>
</Codenesium>*/