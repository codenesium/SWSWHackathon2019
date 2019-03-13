using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ArtLaShipNS.Api.DataAccess
{
	public partial interface IEmailRepository
	{
		Task<Email> Create(Email item);

		Task Update(Email item);

		Task Delete(int id);

		Task<Email> Get(int id);

		Task<List<Email>> All(int limit = int.MaxValue, int offset = 0, string query = "");

		Task<List<Email>> ByArtistId(int artistId, int limit = int.MaxValue, int offset = 0);

		Task<Artist> ArtistByArtistId(int artistId);
	}
}

/*<Codenesium>
    <Hash>4e0b0259b6f4a8bed824fa99ad7d0de2</Hash>
</Codenesium>*/