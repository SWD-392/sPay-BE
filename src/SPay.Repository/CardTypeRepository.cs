using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Card.Request;
using SPay.BO.DTOs.CardType.Request;
using SPay.Repository.Enum;

namespace SPay.Repository
{
	public interface ICardTypeRepository
	{
		Task<IList<CardType>> GetListCardTypeAsync(GetListCardTypeRequest request);
		Task<CardType> GetCardTypeByKeyAsync(string key);
		Task<IList<CardTypesStoreCategory>> GetRelationListAsync();
		Task<bool> DeleteCardTypeAsync(CardType cardTypeExisted);
		Task<bool> CreateCardTypeAsync(CardType item, string storeCateKey);
		Task<bool> UpdateCardTypeAsync(string key, CardType updatedCardType);
	}
	public class CardTypeRepository : ICardTypeRepository
	{
		private readonly SpayDBContext _context;

		public CardTypeRepository(SpayDBContext context)
		{
			_context = context;
		}

		public async Task<bool> CreateCardTypeAsync(CardType item, string storeCateKey)
		{
			_context.CardTypes.Add(item);
			_context.CardTypesStoreCategories.Add(new CardTypesStoreCategory() { CardTypeKey = item.CardTypeKey, StoreCateKey = storeCateKey });
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> DeleteCardTypeAsync(CardType cardTypeExisted)
		{
			cardTypeExisted.Status = (byte)BasicStatusEnum.Deleted;
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<CardType> GetCardTypeByKeyAsync(string key)
		{
			var response = await _context.CardTypes.SingleOrDefaultAsync(
											ct => ct.CardTypeKey.Equals(key)
											&& !ct.Status.Equals((byte)BasicStatusEnum.Deleted));
			if (response == null)
			{
				throw new Exception($"Card type with key '{key}' not found.");
			}
			return response;
		}

		public async Task<IList<CardType>> GetListCardTypeAsync(GetListCardTypeRequest request)
		{

			var query = _context.CardTypes
				.Where(ct => !ct.Status.Equals((byte)BasicStatusEnum.Deleted))
				.OrderByDescending(ct => ct.InsDate)
				.AsQueryable();
			if (!string.IsNullOrEmpty(request.CardTypeName))
			{
				query = query.Where(ct => ct.CardTypeName.Contains(request.CardTypeName));
			}
			if (!string.IsNullOrEmpty(request.StoreCateKey))
			{
				var listCardTypeKeys = await _context.CardTypesStoreCategories
					.Where(r => r.StoreCateKey.Equals(request.StoreCateKey))
					.Select(r => r.CardTypeKey)
					.ToListAsync();

				query = query.Where(c => listCardTypeKeys.Contains(c.CardTypeKey));
			}

			return await query.ToListAsync();
		}

		public async Task<IList<CardTypesStoreCategory>> GetRelationListAsync()
		{
			return await _context.CardTypesStoreCategories.ToListAsync();
		}

		public async Task<bool> UpdateCardTypeAsync(string key, CardType updatedCardType)
		{
			var existedCardType = await _context.CardTypes.SingleOrDefaultAsync(ct => ct.CardTypeKey.Equals(key)
			&& !ct.Status.Equals((byte)BasicStatusEnum.Deleted));
			if (existedCardType == null)
			{
				return false;
			}

			existedCardType.CardTypeName = updatedCardType.CardTypeName;
			existedCardType.Description = updatedCardType.Description;
			if (await _context.SaveChangesAsync() <= 0)
			{
				throw new Exception($"Nothing is update!");
			}
			return true;
		}
	}
}
