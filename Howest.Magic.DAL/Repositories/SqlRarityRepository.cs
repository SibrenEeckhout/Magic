using Howest.MagicCards.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.DAL.Repositories
{
    public class SqlRarityRepository : IRarityRepository
    {
        private readonly mtg_v1Context _db;

        public SqlRarityRepository(mtg_v1Context db)
        {
            _db = db;
        }
        public async Task<IQueryable<Rarity>> GetAllRarities()
        {
            IQueryable<Rarity> rarities = _db.Rarities;
            return rarities;
        }

        public async Task<Rarity> GetRarityById(int id)
        {
            Rarity rarity = _db.Rarities.SingleOrDefault(r => r.Id == id);
            return rarity;
        }
    }
}
