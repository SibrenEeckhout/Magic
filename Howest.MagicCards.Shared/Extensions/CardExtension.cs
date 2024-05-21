using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.Shared.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.Extensions
{
    public static class CardExtension
    {
        public static IQueryable<Card> FilterCards(this IQueryable<Card> cards, CardsFilter filter) =>
        cards.Where(c => (filter.Id == default || c.Id == filter.Id) &&
                     (string.IsNullOrEmpty(filter.Name) || c.Name.Contains(filter.Name)) &&
                     (string.IsNullOrEmpty(filter.SetCode) || c.SetCode == filter.SetCode) &&
                     (!filter.ArtistId.HasValue || c.ArtistId == filter.ArtistId) &&
                     (string.IsNullOrEmpty(filter.RarityCode) || c.RarityCode == filter.RarityCode) &&
                     (string.IsNullOrEmpty(filter.Type) || c.Type.Contains(filter.Type)) &&
                     (string.IsNullOrEmpty(filter.Text) || c.Text.Contains(filter.Text)));


    }
}
