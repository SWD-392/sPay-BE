using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPay.BO.DTOs.Admin.Order.Request;
using SPay.BO.DTOs.Admin.Order.Response;
using SPay.BO.DTOs.Admin;
using SPay.BO.Extention.Paginate;
using SPay.Service.Response;
using AutoMapper;
using SPay.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SPay.BO.DataBase.Models;
using SPay.Repository.Enum;
using SPay.Service.Utils;

namespace SPay.Service
{ 
	public interface IOrderService
	{
		Task<SPayResponse<PaginatedList<OrderResponse>>> GetAllOrdersAsync(GetAllOrderRequest request);
		Task<SPayResponse<OrderResponse>> GetOrderByKeyAsync(string id);
		Task<SPayResponse<PaginatedList<OrderResponse>>> SearchOrderAsync(OrderSearchRequest request);
		Task<SPayResponse<bool>> DeleteOrderAsync(string key);
		Task<SPayResponse<bool>> CreateOrderAsync(CreateOrderRequest request);
	}
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository _repo;
		private readonly IMapper _mapper;

		public OrderService(IOrderRepository repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
		}

		public async Task<SPayResponse<bool>> CreateOrderAsync(CreateOrderRequest request)
		{
			SPayResponse<bool> response = new SPayResponse<bool>();
			try
			{
				if (request == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Request model is null!");
					return response;
				}
				var createOrderInfo = _mapper.Map<Order>(request);
				if (createOrderInfo == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				createOrderInfo.OrderKey = string.Format("{0}{1}", PrefixKeyConstant.ORDER, Guid.NewGuid().ToString().ToUpper());
				createOrderInfo.Date = DateTimeHelper.GetDateTimeNow();
				createOrderInfo.Status = (byte)OrderStatusEnum.Succeeded;
				if (!await _repo.CreateOrderAsync(createOrderInfo))
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				response.Data = true;
				response.Success = true;
				response.Message = "Create a order successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<bool>> DeleteOrderAsync(string key)
		{
			SPayResponse<bool> response = new SPayResponse<bool>();
			try
			{
				var existedOrder = await _repo.GetOrderByKeyAsync(key);
				if (existedOrder == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Cannot find order to delete!");
					return response;
				}
				var success = await _repo.DeleteOrderAsync(existedOrder);
				if (success == false)
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				response.Data = success;
				response.Success = true;
				response.Message = "Order delete successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<PaginatedList<OrderResponse>>> GetAllOrdersAsync(GetAllOrderRequest request)
		{
			var response = new SPayResponse<PaginatedList<OrderResponse>>();
			try
			{
				var orders = await _repo.GetAllOrderTypeAsync();
				if (orders == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Order has no row in database.");
					return response;
				}
				var ordersRes = _mapper.Map<IList<OrderResponse>>(orders);
				var count = 0;
				foreach (var item in ordersRes)
				{
					item.No = ++count;
				}
				response.Data = await ordersRes.ToPaginateAsync(request); ;
				response.Success = true;
				response.Message = "Get Order successfully";
				return response;

			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<OrderResponse>> GetOrderByKeyAsync(string key)
		{
			var response = new SPayResponse<OrderResponse>();
			var order = await _repo.GetOrderByKeyAsync(key);
			try
			{
				if (order == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Order not found!");
					return response;
				}
				var orderRes = _mapper.Map<OrderResponse>(order);

				response.Data = orderRes;
				response.Success = true;
				response.Message = $"Get order key = {orderRes.OrderKey} successfully!";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<PaginatedList<OrderResponse>>> SearchOrderAsync(OrderSearchRequest request)
		{
			var response = new SPayResponse<PaginatedList<OrderResponse>>();
			try
			{
				if(string.IsNullOrEmpty(request.CardName) && string.IsNullOrEmpty(request.FromCustomer) && string.IsNullOrEmpty(request.ToStore))
				{
					SPayResponseHelper.SetErrorResponse(response, "Input at least 1 field to search.");
					return response;
				}
				var orders = await _repo.SearchOrderByNameAsync(request);
				if (orders == null)
				{
					SPayResponseHelper.SetErrorResponse(response, $"Not found order with OrderName={request.CardName} FromCustomer = '{request.FromCustomer}' ToStore = {request.ToStore}.");
					return response;
				}
				var ordersRes = _mapper.Map<IList<OrderResponse>>(orders);
				var count = 0;
				foreach (var item in ordersRes)
				{
					item.No = ++count;
				}
				response.Data = await ordersRes.ToPaginateAsync(request); ;
				response.Success = true;
				response.Message = "Get Order successfully";
				return response;

			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}
	}
}
