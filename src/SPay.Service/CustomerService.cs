using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Admin.Customer.ResponseModel;
using SPay.Repository;
using SPay.Service.Response;

namespace SPay.Service
{
    public interface ICustomerService
    {
        Task<SPayResponse<IList<CustomerResponse>>> GetAllCustomerAsync();
    }
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;
        private readonly IMapper _mapper;
        public CustomerService(ICustomerRepository _repo, IMapper _mapper)
        {
            this._repo = _repo;
            this._mapper = _mapper;
        }
        public async Task<SPayResponse<IList<CustomerResponse>>> GetAllCustomerAsync()
        {
            var result = new SPayResponse<IList<CustomerResponse>>();
            try
            {
                var customerList = await _repo.GetAllCustomer();
                var customerResponse = _mapper.Map<List<CustomerResponse>>(customerList);

                result.Data = customerResponse;
                result.Success = true;
                result.Message = "Customer retrieved successfully";
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
