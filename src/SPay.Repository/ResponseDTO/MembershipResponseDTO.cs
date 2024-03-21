using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPay.BO.DataBase.Models;

namespace SPay.Repository.ResponseDTO
{
	public class MembershipResponseDTO
	{
		public Membership Membership { get; set; }
		public MembershipsWallet MembershipWallet { get; set; }
		public Wallet Wallet { get; set; }
		public Card Card { get; set; }
		public CardType CardType { get; set; }
		public PromotionPackage PromotionPackage { get; set; }
		public CardTypesStoreCategory CardTypeStoreCategory { get; set; }
		public StoreCategory StoreCategory { get; set; }
	}
}
