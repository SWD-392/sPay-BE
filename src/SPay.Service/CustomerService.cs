using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Customer.RespondModel;
using SPay.DAO.ReferenceSRC;
using SPay.Repository;

namespace SPay.Service
{
    public interface ICustomerService
    {
        Task<SPayResponse<IList<GetAllCustomerResponse>>> GetAllCustomerAsync();
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
        public async Task<SPayResponse<IList<GetAllCustomerResponse>>> GetAllCustomerAsync()
        {
            var result = new SPayResponse<IList<GetAllCustomerResponse>>();
            try
            {
                var customerList = await _repo.GetAllCustomer();
                var customerResponse = _mapper.Map<List<GetAllCustomerResponse>>(customerList);

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
