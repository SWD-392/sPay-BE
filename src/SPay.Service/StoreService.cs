using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SPay.BO.DTOs.Admin;
using SPay.BO.DTOs.Admin.Card.Response;
using SPay.BO.DTOs.Admin.Store.Response;
using SPay.Repository;

namespace SPay.Service
{
    public interface IStoreService
    {
        Task<SPayResponse<IList<StoreResponse>>> GetAllStoreInfoAsync();
        Task<SPayResponse<IList<StoreResponse>>> SearchStoreAsync(AdminSearchRequest request);

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
        public async Task<SPayResponse<IList<StoreResponse>>> GetAllStoreInfoAsync()
        {
            var result = new SPayResponse<IList<StoreResponse>>();
            try
            {
                var stores = await _storeRepository.GetAllStoreInfo();
                var storesResponse = _mapper.Map<List<StoreResponse>>(stores);
                var count = 0;
                foreach (var store in storesResponse)
                {
                    store.No = ++count;
                    store.Balance = await _walletRepo.GetBalanceForStore(store.StoreKey);
                }
                result.Data = storesResponse;
                result.Success = true;
                result.Message = "Stores retrieved successfully";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error";
                result.ErrorMessages = new List<string> { ex.Message };
            }
            return result;
        }

        public async Task<SPayResponse<IList<StoreResponse>>> SearchStoreAsync(AdminSearchRequest request)
        {
            var result = new SPayResponse<IList<StoreResponse>>();
            try
            {
                var keyWord = request.keyword.Trim();
                if (string.IsNullOrEmpty(keyWord))
                {
                    result.Success = false;
                    result.Message = "Keyword null or empty!";
                }
                var stores = await _storeRepository.SearchByNameAsync(keyWord);
                var storesRes = _mapper.Map<IList<StoreResponse>>(stores);
                var count = 0;
                foreach (var store in storesRes)
                {
                    store.No = ++count;
                    store.Balance = await _walletRepo.GetBalanceForStore(store.StoreKey);
                }
                result.Data = storesRes;
                result.Success = true;
                result.Message = "Seach card by cart type successfully";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error in : StoreService funciton SearchStoreAsync()";
                result.ErrorMessages = new List<string> { ex.Message };
            }
            return result;
        }
    }
}
