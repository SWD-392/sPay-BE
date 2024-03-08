using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Admin.Card.Request;
using SPay.BO.DTOs.Admin.Card.Response;
using SPay.BO.DTOs.Admin;
using SPay.BO.DTOs.Admin.Customer.ResponseModel;
using SPay.BO.Extention.Paginate;
using SPay.Repository;
using SPay.Service.Response;
using SPay.BO.DTOs.Admin.Customer.RequestModel;
using SPay.BO.DTOs.Admin.User;
using SPay.BO.DTOs.Admin.Wallet;
using SPay.Repository.Enum;
using SPay.BO.DTOs.Admin.Store.Response;

namespace SPay.Service
{
	public interface ICustomerService
	{
		Task<SPayResponse<PaginatedList<CustomerResponse>>> GetAllCustomerAsync(GetAllCustomerRequest request);
		Task<SPayResponse<CustomerResponse>> GetCustomerByKey(string key);
		Task<SPayResponse<PaginatedList<CustomerResponse>>> SearchCustomerAsync(AdminSearchRequest request);
		Task<SPayResponse<bool>> DeleteCustomerAsync(string key);
		Task<SPayResponse<bool>> CreateCustomerAsync(CreateCustomerRequest request);

	}
	public class CustomerService : ICustomerService
	{
		private readonly ICustomerRepository _repo;
		private readonly IMapper _mapper;
		private readonly IWalletService _walletService;
		private readonly ICardService _cardService;

		private readonly IUserService _userService;

		public CustomerService(ICustomerRepository _repo, IMapper _mapper, IWalletService _walletService, IUserService _userService, ICardService _cardService)
		{
			this._repo = _repo;
			this._mapper = _mapper;
			this._walletService = _walletService;
			this._userService = _userService;
			this._cardService = _cardService;
		}

		public async Task<SPayResponse<bool>> CreateCustomerAsync(CreateCustomerRequest request)
		{
			var response = new SPayResponse<bool>();

			try
			{
				var userKey = string.Format("{0}{1}", PrefixKeyConstant.USER, Guid.NewGuid().ToString().ToUpper());
				var user = new CreateUserModel
				{
					UserKey = userKey,
					NumberPhone = request.PhoneNumber,
					Password = request.Password,
					Role = (int)RoleEnum.Customer,
					FullName = request.FullName,
				};

				if (!await _userService.CreateUserAsync(user))
				{
					SPayResponseHelper.SetErrorResponse(response, "Cannot create User, so cannot create customer");
					return response;
				}

				var customerKey = string.Format("{0}{1}", PrefixKeyConstant.CUSTOMER, Guid.NewGuid().ToString().ToUpper());
				var customer = new Customer
				{
					CustomerKey = customerKey,
					Email = request.Email,
					Address = request.Address,
					UserKey = userKey,
				};

				if (!await _repo.CreateCustomerAsync(customer))
				{
					SPayResponseHelper.SetErrorResponse(response, "Cannot create Customer, so cannot create wallet!");
					return response;
				}

				var storeWallet = new CreateWalletModel
				{
					WalletTypeKey = "WT_CUSTOMER",
					CustomerKey = customerKey
				};

				if (!await _walletService.CreateWalletAsync(storeWallet))
				{
					SPayResponseHelper.SetErrorResponse(response, "Store create successfully but fail to create wallet");
					return response;
				}

				response.Data = true;
				response.Success = true;
				response.Message = "Store create successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<bool>> DeleteCustomerAsync(string key)
		{
			SPayResponse<bool> response = new SPayResponse<bool>();
			try
			{
				var existedCus = await _repo.GetCustomerByKeyAsync(key);
				if (existedCus == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Cannot find customer to delete!");
					return response;
				}
				var success = await _repo.DeleteCustomerAsync(existedCus);
				if (success == false)
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				response.Data = success;
				response.Success = true;
				response.Message = "Card delete successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<PaginatedList<CustomerResponse>>> GetAllCustomerAsync(GetAllCustomerRequest request)
		{
			var response = new SPayResponse<PaginatedList<CustomerResponse>>();
			try
			{
				var customerList = await _repo.GetAllCustomerAsync();
				if (customerList.Count <= 0)
				{
					SPayResponseHelper.SetErrorResponse(response, "No reacord in database");
					return response;
				}
				var customerResponse = _mapper.Map<List<CustomerResponse>>(customerList);
				var count = 0;
				foreach (var customer in customerResponse)
				{
					customer.No = ++count;
					customer.Balance = await _walletService.GetBalanceOfUserAsync(new GetBalanceModel { CustomerKey = customer.CustomerKey});
					customer.NumOfCards = await _cardService.CountCardByUserKey(customer.CustomerKey);
				}
				response.Data = await customerResponse.ToPaginateAsync(request);
				response.Success = true;
				response.Message = "Get all card successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<CustomerResponse>> GetCustomerByKey(string key)
		{
			var response = new SPayResponse<CustomerResponse>();

			try
			{
				var customer = await _repo.GetCustomerByKeyAsync(key);

				if (customer == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Store not found.");
					return response;
				}

				response.Data = _mapper.Map<CustomerResponse>(customer);
				response.Success = true;
				response.Message = $"Get Store key = {customer.CustomerKey} successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}

			return response;
		}

		public async Task<SPayResponse<PaginatedList<CustomerResponse>>> SearchCustomerAsync(AdminSearchRequest request)
		{
			var response = new SPayResponse<PaginatedList<CustomerResponse>>();
			try
			{
				var keyWord = request.Keyword.Trim();
				if (string.IsNullOrEmpty(keyWord))
				{
					SPayResponseHelper.SetErrorResponse(response, "Key word name must not empty or null!");
					return response;
				}
				var customerList = await _repo.SearchCustomerByNameAsync(keyWord);
				if (customerList.Count <= 0)
				{
					SPayResponseHelper.SetErrorResponse(response, "No reacord in database");
					return response;
				}
				var customerResponse = _mapper.Map<List<CustomerResponse>>(customerList);
				var count = 0;
				foreach (var customer in customerResponse)
				{
					customer.No = ++count;
					customer.Balance = await _walletService.GetBalanceOfUserAsync(new GetBalanceModel { CustomerKey = customer.CustomerKey });
					customer.NumOfCards = await _cardService.CountCardByUserKey(customer.CustomerKey); 
				}
				response.Data = await customerResponse.ToPaginateAsync(request);
				response.Success = true;
				response.Message = "Get all card successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}
	}
}
