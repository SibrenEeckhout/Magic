using Howest.MagicCards.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = Howest.MagicCards.DAL.Models.Type;

namespace Howest.MagicCards.DAL.Repositories
{
    public class SqlTypeRepository : ITypeRepository
    {
        private readonly mtg_v1Context _db;

        public SqlTypeRepository(mtg_v1Context db)
        {
            _db = db;
        }

        public async Task<IQueryable<Type>> GetAllCardTypes()
        {
            IQueryable<Type> types = _db.Types;
            return types;
        }

        public async Task<Type> GetType(int id)
        {
            Type type = _db.Types.SingleOrDefault(c => c.Id == id);
            return type;
        }
    }
}
