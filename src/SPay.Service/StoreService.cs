using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SPay.BO.DTOs.Admin.Store.Response;
using SPay.BO.DTOs.Customer.RespondModel;
using SPay.Repository;

namespace SPay.Service
{
    public interface IStoreService
    {
        Task<SPayResponse<IList<GetAllStoreResponse>>> GetAllStoreInfoAsync();
    }
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;
        public StoreService(IStoreRepository _storeRepository, IMapper _mapper)
        {
            this._storeRepository = _storeRepository;
            this._mapper = _mapper;
        }
        public async Task<SPayResponse<IList<GetAllStoreResponse>>> GetAllStoreInfoAsync()
        {
            var result = new SPayResponse<IList<GetAllStoreResponse>>();
            try
            {
                var stores = await _storeRepository.GetAllStoreInfo();
                var storesResponse = _mapper.Map<List<GetAllStoreResponse>>(stores);
                var count = 0;
                foreach (var store in storesResponse)
                {
                    store.No = ++count;
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
    }
}
