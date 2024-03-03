using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPay.BO.DataBase.Models;

namespace SPay.Repository
{
    public interface ICardRepository
    {
        Task<IList<Card>> GetAllAsync();
    }
    public class CardRepository : ICardRepository
    {
        private readonly SPayDbContext _context;
        public CardRepository(SPayDbContext _context)
        {
            this._context = _context;
        }
        public async Task<IList<Card>> GetAllAsync()
        {
            var cards = await _context.Cards.Include(c => c.CardTypeKeyNavigation).ToListAsync();
            return cards;
        }
    }
}
