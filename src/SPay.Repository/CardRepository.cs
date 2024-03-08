using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPay.BO.DataBase.Models;
using SPay.Repository.Enum;
using static System.Formats.Asn1.AsnWriter;

namespace SPay.Repository
{
    public interface ICardRepository
    {
        Task<IList<Card>> GetAllAsync();
		Task<Card> GetCardByIdAsync(string key);
		Task<IList<Card>> SearchCardByNameAsync(string keyWord);
        Task<bool> DeleteCardAsync(Card existedCard);
        Task<bool> IsDeleted(string key);
		Task<bool> CreateCardAsync(Card card);


	}
	public class CardRepository : ICardRepository
    {
        private readonly SPayDbContext _context;
        public CardRepository(SPayDbContext _context)
        {
            this._context = _context;
        }

		public async Task<bool> DeleteCardAsync(Card existedCard)
		{
            existedCard.Status = (byte)CardStatusEnum.Deleted;
            await _context.SaveChangesAsync();
            return true;
		}

        public async Task<bool> IsDeleted(string key)
        {
            var status = await _context.Cards
	                            .Where(c => c.CardKey.Equals(key))
	                            .Select(c => c.Status)
	                            .FirstOrDefaultAsync();
            if(status == (byte)CardStatusEnum.Deleted)
            {
                return true;
            }
            return false;
        }
		public async Task<IList<Card>> GetAllAsync()
        {
            var cards = await _context.Cards
                .Where(c => c.Status != (byte)CardStatusEnum.Deleted)
                .Include(c => c.CardTypeKeyNavigation)
                .ToListAsync();
            return cards;
        }

		public async Task<Card> GetCardByIdAsync(string key)
		{
			var card = await _context.Cards
	                    .Include(c => c.CardTypeKeyNavigation)
	                    .FirstOrDefaultAsync(c => c.CardKey == key && c.Status != (byte)CardStatusEnum.Deleted);
			return card;
		}

		public async Task<IList<Card>> SearchCardByNameAsync(string keyWord)
        {
            var cards = await _context.Cards
                .Where(c => 
                c.CardName.ToLower().Contains(keyWord.ToLower())
				&& c.Status != (byte)CardStatusEnum.Deleted)
                .Include(c => c.CardTypeKeyNavigation).ToListAsync();
            return cards;
        }

		public async Task<bool> CreateCardAsync(Card card)
		{
			_context.Cards.Add(card);
			return await _context.SaveChangesAsync() > 0;
		}
	}
}
