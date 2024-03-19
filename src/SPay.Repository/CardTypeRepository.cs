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
	public interface ICardTypeRepository
	{
		Task<IList<CardType>> GetListCardTypeAsync();
		Task<CardType> GetCardTypeByKeyAsync(string key);
		Task<bool> DeleteCardTypeAsync(CardType cardTypeExisted);
		Task<bool> CreateCardTypeAsync(CardType item);
		Task<bool> UpdateCardTypeAsync(string key, CardType updatedCardType);
	}
	public class CardTypeRepository : ICardTypeRepository
	{
		private readonly SpayDBContext _context;

		public CardTypeRepository(SpayDBContext context)
		{
			_context = context;
		}

		public async Task<bool> CreateCardTypeAsync(CardType item)
		{
			_context.CardTypes.Add(item);
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

			return response ?? new CardType();
		}

		public async Task<IList<CardType>> GetListCardTypeAsync()
		{
			var response = await _context.CardTypes
				.Where(pp => !pp.Status.Equals((byte)BasicStatusEnum.Deleted))
				.ToListAsync();
			return response;
		}

		public async Task<bool> UpdateCardTypeAsync(string key, CardType updatedPackage)
		{
			var existedCardType = await _context.CardTypes.SingleOrDefaultAsync(p => p.CardTypeKey.Equals(key)
			&& !p.Status.Equals((byte)BasicStatusEnum.Deleted));
			if (existedCardType == null)
			{
				return false;
			}

			existedCardType.CardTypeName = updatedPackage.CardTypeName;
			existedCardType.Description = updatedPackage.Description;
			return await _context.SaveChangesAsync() > 0;
		}
	}
}
