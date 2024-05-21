using Howest.MagicCards.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.DAL.Repositories
{
    public interface IRarityRepository
    {
        Task<IQueryable<Rarity>>? GetAllRarities();
        Task<Rarity>? GetRarityById(int id);
    }
}
