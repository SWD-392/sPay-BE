using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Membership.Request;
using SPay.BO.DTOs.Order.Request;
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
			if (!string.IsNullOrEmpty(request.StoreCateKey))
			{
				query = query.Where(s => s.StoreCategory.StoreCategoryKey.Equals(request.UserKey));
			}			
			return await query.OrderByDescending(t => t.Wallet.InsDate).ToListAsync();
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
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					

					var walletInfo = new Wallet();
					// Thêm một wallet mới và một membershipsWallet mới vào context
					if (item.CardKey != null)
					{
						var cardInfo = await _context.Cards.Include(c => c.PromotionPackageKeyNavigation).SingleOrDefaultAsync(c => c.CardKey.Equals(item.CardKey));

						if (cardInfo == null)
						{
							throw new Exception($"Card with key '{item.CardKey}' not found.");
						}

						//Set due date for using membership
						item.ExpiritionDate = GetDateTimeNow().AddDays(cardInfo.PromotionPackageKeyNavigation.NumberDate);
						item.IsDefaultMembership = false;
						//Create wallet following card info
						walletInfo.Balance = cardInfo.PromotionPackageKeyNavigation.UsaebleAmount;
						walletInfo.Description = string.Format(Constant.Wallet.DES_FOR_OTHER_MEMBERSHIP, cardInfo.CardName);
					}
					else
					{
						item.IsDefaultMembership = true;
						walletInfo.Balance = Constant.Wallet.DEFAULT_BALANCE;
						walletInfo.Description = Constant.Wallet.DES_FOR_DEFAULT_MEMBERSHIP;
					}
					walletInfo.WalletKey = string.Format("{0}{1}", PrefixKeyConstant.WALLET, Guid.NewGuid().ToString().ToUpper());
					walletInfo.Status = (byte)WalletStatusEnum.Active;
					walletInfo.InsDate = GetDateTimeNow();
					// Thêm membership vào context
					_context.Memberships.Add(item);

					//Thêm wallet
					_context.Wallets.Add(walletInfo);

					// Tạo mqh giữa membership và wallet
					var membershipWalletRelation = new MembershipsWallet { MembershipKey = item.MembershipKey, WalletKey = walletInfo.WalletKey };
					_context.MembershipsWallets.Add(membershipWalletRelation);

					// Lưu các thay đổi vào cơ sở dữ liệu
					await _context.SaveChangesAsync();

					// Commit transaction nếu thành công
					await transaction.CommitAsync();

					// Trả về true nếu thành công
					return true;
				}
				catch (Exception ex)
				{
					await transaction.RollbackAsync();
					throw new Exception(ex.Message + "so all change was roll back");
				}
			}
		}
		private DateTime GetDateTimeNow()
		{
			return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
		}

	}
}
