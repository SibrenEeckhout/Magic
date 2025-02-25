﻿using Howest.MagicCards.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Howest.MagicCards.DAL.Repositories
{
    public interface ICardRepository
    {
        Task<IQueryable<Card>>? GetAllCards(); 
        Task<Card>? GetCardById(int id);
        IQueryable<Card> GetCardsByArtist(int id);
    }
}
