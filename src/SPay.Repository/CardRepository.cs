using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPay.BO.DataBase.Models;
using SPay.Repository.Enum;

namespace SPay.Repository
{
    public interface ICardRepository
    {
        Task<IList<Card>> GetAllAsync();
        Task<IList<Card>> SearchCardByNameAsync(string keyWord);

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
            var cards = await _context.Cards
                .Where(c => c.Status != (byte)CardStatusEnum.Deleted)
                .Include(c => c.CardTypeKeyNavigation)
                .Include(c => c.Deposits)
                .ToListAsync();
            return cards;
        }

        public async Task<IList<Card>> SearchCardByNameAsync(string keyWord)
        {
            var cards = await _context.Cards
                .Where(c => c.CardTypeKeyNavigation.Name.ToLower().Contains(keyWord.ToLower()))
                .Include(c => c.CardTypeKeyNavigation).ToListAsync();
            return cards;
        }
    }
}
