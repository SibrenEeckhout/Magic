using Howest.MagicCards.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = Howest.MagicCards.DAL.Models.Type;

namespace Howest.MagicCards.DAL.Repositories
{
    public interface ITypeRepository
    {
        Task<IQueryable<Type>>? GetAllCardTypes();
        Task<Type>? GetType(int id);
    }
}
