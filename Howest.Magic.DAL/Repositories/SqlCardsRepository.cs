using Howest.MagicCards.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.DAL.Repositories
{
    public class SqlCardsRepository : ICardRepository
    {
        private readonly mtg_v1Context _db;

        public SqlCardsRepository(mtg_v1Context db)
        {
            _db = db;
        }

        public async Task<IQueryable<Card>> GetAllCards()
        {
            IQueryable<Card> allCards = _db.Cards;

            return allCards;
        }


        public async Task<Card> GetCardById(int id)
        {
            Card? singleCard = _db.Cards
                .SingleOrDefault(c => c.Id == id);
            return singleCard;
        }

        public IQueryable<Card> GetCardsByArtist(int id)
        {
            IQueryable<Card> allCards = _db.Cards.Where(card => card.ArtistId == id);

            return allCards;
        }
    }
}
