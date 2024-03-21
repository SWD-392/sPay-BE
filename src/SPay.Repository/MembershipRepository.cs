using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Membership.Request;
using SPay.Repository.Enum;
using SPay.Repository.ResponseDTO;

namespace SPay.Repository
{
	public interface IMembershipRepository
	{
		Task<IList<MembershipResponseDTO>> GetListMembershipAsync(GetListMembershipRequest request);
		Task<MembershipResponseDTO> GetMembershipByKeyAsync(string key);
		Task<bool> CreateMembershipAsync(Membership item);
	}
	public class MembershipRepository : IMembershipRepository
	{
		private readonly SpayDBContext _context;

		public MembershipRepository(SpayDBContext context)
		{
			_context = context;
		}

		public async Task<IList<MembershipResponseDTO>> GetListMembershipAsync(GetListMembershipRequest request)
		{

			var query = from m in _context.Memberships
						join w in _context.MembershipsWallets on m.MembershipKey equals w.MembershipKey
						join wl in _context.Wallets on w.WalletKey equals wl.WalletKey
						join c in _context.Cards on m.CardKey equals c.CardKey
						join ct in _context.CardTypes on c.CardTypeKey equals ct.CardTypeKey
						join p in _context.PromotionPackages on c.PromotionPackageKey equals p.PromotionPackageKey
						join cs in _context.CardTypesStoreCategories on c.CardTypeKey equals cs.CardTypeKey
						join sc in _context.StoreCategories on cs.StoreCateKey equals sc.StoreCategoryKey
						select new MembershipResponseDTO
						{
							Membership = m,
							MembershipWallet = w,
							Wallet = wl,
							Card = c,
							CardType = ct,
							PromotionPackage = p,
							CardTypeStoreCategory = cs,
							StoreCategory = sc
						};

			if (!string.IsNullOrEmpty(request.UserKey))
			{
				query = query.Where(s => s.Membership.UserKey.Equals(request.UserKey));
			}

			return await query.ToListAsync();
		}

		public async Task<MembershipResponseDTO> GetMembershipByKeyAsync(string key)
		{
			var query = from m in _context.Memberships
						join w in _context.MembershipsWallets on m.MembershipKey equals w.MembershipKey
						join wl in _context.Wallets on w.WalletKey equals wl.WalletKey
						join c in _context.Cards on m.CardKey equals c.CardKey
						join ct in _context.CardTypes on c.CardTypeKey equals ct.CardTypeKey
						join p in _context.PromotionPackages on c.PromotionPackageKey equals p.PromotionPackageKey
						join cs in _context.CardTypesStoreCategories on c.CardTypeKey equals cs.CardTypeKey
						join sc in _context.StoreCategories on cs.StoreCateKey equals sc.StoreCategoryKey
						where m.MembershipKey.Equals(key)
						select new MembershipResponseDTO
						{
							Membership = m,
							MembershipWallet = w,
							Wallet = wl,
							Card = c,
							CardType = ct,
							PromotionPackage = p,
							CardTypeStoreCategory = cs,
							StoreCategory = sc
						};
			var membership = await query.SingleOrDefaultAsync();
			if (membership == null)
			{
				throw new Exception($"Membership with key '{key}' not found.");
			}
			return membership;
		}

		public async Task<bool> CreateMembershipAsync(Membership item)
		{
			_context.Memberships.Add(item);
			return await _context.SaveChangesAsync() > 0;
		}
	}
}
