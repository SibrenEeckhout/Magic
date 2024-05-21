using AutoMapper.Internal.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.Filters
{
    public class CardsFilter
    {
        public long? Id { get; init; }
        public string Name { get; init; } = null;

        public string SetCode { get; init; } = null;
        public long? ArtistId { get; init; }
        public string RarityCode { get; init; } = null;
        public string Type { get; init; } = null;
        public string Text { get; init; } = null;

        public CardsFilter()
        {

        }
        public CardsFilter(long id, string name, string setCode, long? artistId, string rarityCode, string type, string text)
        {
            Id = id;
            Name = name;
            SetCode = setCode;
            ArtistId = artistId;
            RarityCode = rarityCode;
            Type = type;
            Text = text;
        }
    }
}
