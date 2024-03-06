using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SPay.BO.DTOs;
using SPay.BO.DTOs.Admin;
using SPay.BO.DTOs.Admin.Card.Response;
using SPay.BO.DTOs.Admin.Store.Response;
using SPay.BO.Extention.Paginate;
using SPay.Repository;

namespace SPay.Service
{
	public interface IStoreService
	{
		Task<PaginatedList<StoreResponse>> GetAllStoreInfoAsync(PagingRequest request);
		Task<PaginatedList<StoreResponse>> SearchStoreAsync(AdminSearchRequest request);

	}
	public class StoreService : IStoreService
	{
		private readonly IStoreRepository _storeRepository;
		private readonly IMapper _mapper;
		private readonly IWalletRepository _walletRepo;
		public StoreService(IStoreRepository _storeRepository, IMapper _mapper, IWalletRepository walletRepo)
		{
			this._storeRepository = _storeRepository;
			this._mapper = _mapper;
			_walletRepo = walletRepo;
		}
		public async Task<PaginatedList<StoreResponse>> GetAllStoreInfoAsync(PagingRequest request)
		{

			var stores = await _storeRepository.GetAllStoreInfo();
			var storesResponse = _mapper.Map<List<StoreResponse>>(stores);
			var count = 0;
			foreach (var store in storesResponse)
			{
				store.No = ++count;
				store.Balance = await _walletRepo.GetBalanceForStore(store.StoreKey);
			}
			return await storesResponse.ToPaginateAsync(request);
		}

		public async Task<PaginatedList<StoreResponse>> SearchStoreAsync(AdminSearchRequest request)
		{
			var keyWord = request.Keyword.Trim();
			if (string.IsNullOrEmpty(keyWord))
			{
				throw new ArgumentNullException(nameof(keyWord));
			}
			var stores = await _storeRepository.SearchByNameAsync(keyWord);
			var storesRes = _mapper.Map<IList<StoreResponse>>(stores);
			var count = 0;
			foreach (var store in storesRes)
			{
				store.No = ++count;
				store.Balance = await _walletRepo.GetBalanceForStore(store.StoreKey);
			}
			return await storesRes.ToPaginateAsync(request);

		}
	}
}
