using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.DTO
{
    public record TypeReadDTO
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public string Type1 { get; init; }
           
    }
}
