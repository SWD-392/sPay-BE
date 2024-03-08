using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Admin.Wallet;
using SPay.Repository;
using SPay.Repository.Enum;
using SPay.Service.Utils;

namespace SPay.Service
{
	public interface IWalletService
	{
		Task<bool> CreateWalletAsync(CreateWalletModel model);
		Task<decimal?> GetBalanceOfUserAsync(GetBalanceModel model);
		Task<IList<string>> GetListCardByUserKeyAsync(string userKey);

	}
	public class WalletService : IWalletService
	{
		private readonly IWalletRepository _repo;
        public WalletService(IWalletRepository _repo)
        {
            this._repo = _repo;
        }
        public async Task<bool> CreateWalletAsync(CreateWalletModel model)
		{
			var wallet = new Wallet
			{
				WalletKey = string.Format("{0}{1}", PrefixKeyConstant.WALLET, Guid.NewGuid().ToString().ToUpper()),
				WalletTypeKey = model.WalletTypeKey,
				CardKey = model.CardKey,
				StoreKey = model.StoreKey,
				CustomerKey = model.CustomerKey,
				Balance = model.Balance,
				Status = (byte)WalletStatusEnum.Active,
				CreateAt = DateTimeHelper.GetDateTimeNow(),
			};
			return await _repo.CreateWalletAsync(wallet);
		}

		public async Task<decimal?> GetBalanceOfUserAsync(GetBalanceModel model)
		{
			var wallet = await _repo.GetBalanceOfUserAsync(model);
			if(wallet == null)
			{
				return 0;
			}
			return wallet.Balance;
		}

		public async Task<IList<string>> GetListCardByUserKeyAsync(string userKey)
		{
			var wallets = await _repo.GetWalletCardByUserKeyAsync(userKey);
			var result = new List<string>();
			foreach(var wallet in wallets)
			{
				result.Add(wallet.CardKey);
			}
			return result;
		}
	}
}
