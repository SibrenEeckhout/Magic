using Howest.MagicCards.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.DAL.Repositories
{
    public class SqlArtistRepository : IArtistRepository
    {
        private readonly mtg_v1Context _db;

        public SqlArtistRepository(mtg_v1Context db)
        {
            _db = db;
        }

        public async Task<IQueryable<Artist>> GetAllArtists()
        {
            IQueryable<Artist> artists= _db.Artists;
            return artists;
        }

        public async Task<Artist> GetArtistById(int id)
        {
            Artist? singleArtist = _db.Artists
                .SingleOrDefault(a => a.Id == id);
            return singleArtist;
        }
    }
}
