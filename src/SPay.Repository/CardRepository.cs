using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Card.Request;
using SPay.Repository.Enum;
using static System.Formats.Asn1.AsnWriter;

namespace SPay.Repository
{
    public interface ICardRepository
    {
		Task<IList<Card>> GetListCardAsync(GetListCardRequest request);
		Task<Card> GetCardByKeyAsync(string key);
		Task<Card> GetCardByMemberShipKeyAsync(string key);
		Task<bool> DeleteCardAsync(Card cardExisted);
		Task<bool> CreateCardAsync(Card item);
		Task<bool> UpdateCardAsync(string key, Card updatedCard);
	}
	public class CardRepository: ICardRepository
    {
        private readonly SpayDBContext _context;
        public CardRepository(SpayDBContext _context)
        {
            this._context = _context;
        }

		public async Task<bool> CreateCardAsync(Card item)
		{
			_context.Cards.Add(item);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> DeleteCardAsync(Card cardExisted)
		{
			cardExisted.Status = (byte)BasicStatusEnum.Deleted;
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<Card> GetCardByKeyAsync(string key)
		{
			var response = await _context.Cards.Include(c => c.CardTypeKeyNavigation)
									.Include(c => c.PromotionPackageKeyNavigation)
									.SingleOrDefaultAsync(ct => ct.CardKey.Equals(key)
														&& !ct.Status.Equals((byte)BasicStatusEnum.Deleted));
			if (response == null)
			{
				throw new Exception($"Card with key '{key}' not found.");
			}
			return response;
		}

		public async Task<Card> GetCardByMemberShipKeyAsync(string membershipKey)
		{
			var card = (await _context.Memberships.SingleOrDefaultAsync(m => m.MembershipKey.Equals(membershipKey)));
			if(card == null)
			{
				throw new Exception($"User dont have membership with key {membershipKey}");
			}
			var key = card.CardKey;

			var response = await _context.Cards.Include(c => c.CardTypeKeyNavigation)
									.Include(c => c.PromotionPackageKeyNavigation)
									.SingleOrDefaultAsync(ct => ct.CardKey.Equals(key)
														&& !ct.Status.Equals((byte)BasicStatusEnum.Deleted));
			if (response == null)
			{
				throw new Exception($"Card with key '{key}' not found.");
			}
			return response;
		}

		public async Task<IList<Card>> GetListCardAsync(GetListCardRequest request)
		{
			var query = _context.Cards
				.Include(c => c.CardTypeKeyNavigation)
				.Include(c => c.PromotionPackageKeyNavigation)
				.Where(pp => !pp.Status.Equals((byte)BasicStatusEnum.Deleted))
				.OrderByDescending(e => e.InsDate)
				.AsQueryable();
			if (!string.IsNullOrEmpty(request.CardTypeKey))
			{
				query = query.Where(p => p.CardTypeKey.Equals(request.CardTypeKey));
			}

			if (!string.IsNullOrEmpty(request.PromotionPackageKey))
			{
				query = query.Where(p => p.PromotionPackageKey.Equals(request.PromotionPackageKey));
			}

			return await query.ToListAsync();
		}

		public async Task<bool> UpdateCardAsync(string key, Card updatedCard)
		{
			var existedCard = await _context.Cards.SingleOrDefaultAsync(c => c.CardKey.Equals(key) 
													&& !c.Status.Equals((byte)BasicStatusEnum.Deleted));
			if (existedCard == null)
			{
				return false;
			}

			existedCard.CardTypeKey = updatedCard.CardTypeKey;
			existedCard.CardName = updatedCard.CardName;
			existedCard.Description = updatedCard.Description;
			existedCard.PromotionPackageKey = updatedCard.PromotionPackageKey;
			if (await _context.SaveChangesAsync() <= 0)
			{
				throw new Exception($"Nothing is update!");
			}
			return true;
		}
	}
}
