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
	}
}
