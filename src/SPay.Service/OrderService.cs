using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPay.BO.Extention.Paginate;
using SPay.Service.Response;
using AutoMapper;
using SPay.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SPay.BO.DataBase.Models;
using SPay.Repository.Enum;
using SPay.Service.Utils;
using SPay.BO.DTOs.Order.Request;
using SPay.BO.DTOs.Order.Response;
using SPay.BO.DTOs.CardType.Request;

namespace SPay.Service
{
	public interface IOrderService
	{
		Task<SPayResponse<PaginatedList<OrderResponse>>> GetAllOrdersAsync(GetListOrderRequest request);
		Task<SPayResponse<OrderResponse>> GetOrderByKeyAsync(string id);
		Task<SPayResponse<bool>> CreateOrderAsync(CreateOrderRequest request);
		Task<SPayResponse<bool>> DeleteOrderAsync(string key);
	}
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository _repo;
		private readonly IUserRepository _repoUser;
		private readonly ICardRepository _repoCard;
		private readonly ICardTypeRepository _repoCardType;
		private readonly IStoreRepository _repoStore;
		private readonly IMembershipRepository _repoMembership;
		private readonly ITransactionRepository _repoTrans;


		private readonly IMapper _mapper;

		public OrderService(IOrderRepository repo, IUserRepository repoU, ICardRepository repoC, IStoreRepository repoS, ICardTypeRepository repoCt, IMembershipRepository repoMb, ITransactionRepository repoTrans, IMapper mapper)
		{
			_repo = repo;
			_repoUser = repoU;
			_repoCard = repoC;
			_mapper = mapper;
			_repoCardType = repoCt;
			_repoStore = repoS;
			_repoMembership = repoMb;
			_repoTrans = repoTrans;
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
				var valid  =  await IsValidCardTypeStoreCate(request);
				if(!valid)
				{
					throw new Exception("The store not valid, please try again");
				}

				if (await IsExpiriedDateMembership(request.MembershipKey))
				{
					throw new Exception("The membership was expiried, please choose another membership");
				}

				if (await IsValidBalanceMembership(request))
				{
					throw new Exception("The balance of membership was not enough please using another to purcharse");
				}

				var createOrderInfo = _mapper.Map<Order>(request);
				if (createOrderInfo == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Mapping have result null");
					return response;
				}
				createOrderInfo.OrderKey = string.Format("{0}{1}", PrefixKeyConstant.ORDER, Guid.NewGuid().ToString().ToUpper());
				createOrderInfo.InsDate = DateTimeHelper.GetDateTimeNow();
				createOrderInfo.Status = (byte)OrderStatusEnum.Processing;

				//Reduce money of membership
				if (!(await _repo.ProcessMoney(await _repoMembership.GetMembershipByKeyAsync(request.MembershipKey), request)))
				{
					createOrderInfo.Status = (byte)OrderStatusEnum.Failed;
					SPayResponseHelper.SetErrorResponse(response, "Error when process money");
				}
				else
				{
					createOrderInfo.Status = (byte)OrderStatusEnum.Succeeded;
				}

				if (!await _repo.CreateOrderAsync(createOrderInfo))
				{
					throw new Exception("Create order failed!");
				}

				var transactionOrder = new Transaction();
				transactionOrder.TransactionKey = string.Format("{0}{1}", PrefixKeyConstant.TRANSACTION, Guid.NewGuid().ToString().ToUpper());
				transactionOrder.Status = createOrderInfo.Status;
				transactionOrder.OrderKey = createOrderInfo.OrderKey;
				transactionOrder.InsDate = createOrderInfo.InsDate;
				if (!await _repoTrans.CreateTransactionAsync(transactionOrder))
				{
					throw new Exception("Create transaction failed!");
				};

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

		public async Task<SPayResponse<PaginatedList<OrderResponse>>> GetAllOrdersAsync(GetListOrderRequest request)
		{
			var response = new SPayResponse<PaginatedList<OrderResponse>>();
			try
			{
				var orders = await _repo.GetAllOrderTypeAsync(request);
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
					item.FromUserName = (await _repoUser.GetUserByKeyAsync(item.FromUserName)).Fullname;
					item.ByCardName = (await _repoCard.GetCardByKeyAsync(item.ByCardName)).CardName;
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
				orderRes.FromUserName = (await _repoUser.GetUserByKeyAsync(orderRes.FromUserName)).Fullname;
				orderRes.ByCardName = (await _repoCard.GetCardByKeyAsync(orderRes.ByCardName)).CardName;

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


		private async Task<bool> IsValidCardTypeStoreCate(CreateOrderRequest request)
		{
			var store = await _repoStore.GetStoreByKeyAsync(request.StoreKey);
			var storeCate = store.StoreCateKey;

			var cardNow = await _repoCard.GetCardByMemberShipKeyAsync(request.MembershipKey);
			var cardTypeNow = cardNow.CardTypeKey;

			var listRelation = await _repoCardType.GetRelationListAsync();

			var isRelationExist = listRelation.Any(r => r.StoreCateKey == storeCate && r.CardTypeKey == cardTypeNow);

			return isRelationExist;
		}

		private async Task<bool> IsExpiriedDateMembership(string memberShipKey)
		{
			var memberShip = await _repoMembership.GetMembershipByKeyAsync(memberShipKey);
			var expiriedDate = memberShip.Membership.ExpiritionDate;
			return expiriedDate < DateTimeHelper.GetDateTimeNow();
		}

		private async Task<bool> IsValidBalanceMembership(CreateOrderRequest request)
		{
			var memberShip = await _repoMembership.GetMembershipByKeyAsync(request.MembershipKey);
			var balance = memberShip.Wallet.Balance; //Số dư
			if(balance <= 0)
			{
				return false;
			}
			return balance <= request.TotalAmount; //số dư vs số tiền tính
		}
	}
}
