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
		Task<IList<CardType>> GetAllCardTypeAsync();
		Task<IList<Card>> GetAllAsync();
		Task<Card> GetCardByKeyAsync(string key);
		Task<IList<Card>> SearchCardByNameAsync(string keyWord);
        Task<bool> DeleteCardAsync(Card existedCard);
		Task<bool> CreateCardAsync(Card card);
		Task<IList<Card>> GetListCardByCustomerKey(string customerKey);
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
            return await _context.SaveChangesAsync() > 0;
		}

		public async Task<IList<Card>> GetAllAsync()
        {
            var cards = await _context.Cards
                .Where(c => c.Status != (byte)CardStatusEnum.Deleted)
                .Include(c => c.CardTypeKeyNavigation)
                .ToListAsync();
            return cards;
        }

		public async Task<Card> GetCardByKeyAsync(string key)
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

		public async Task<IList<CardType>> GetAllCardTypeAsync()
		{
			return await _context.CardTypes.ToListAsync();
		}

		public async Task<IList<Card>> GetListCardByCustomerKey(string customerKey)
		{
			var result = new List<Card>();
			var keyList = await _context.Wallets.Where(w => w.CustomerKey.Equals(customerKey) && w.CardKey != null).Select(w => w.CardKey).ToListAsync();
			foreach (var cardKey in keyList)
			{
				result.Add(await GetCardByKeyAsync(cardKey));
			}
			return result;
		}
	}
}
