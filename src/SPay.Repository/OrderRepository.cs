using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Admin.Order.Request;
using SPay.Repository.Enum;

namespace SPay.Repository
{
	public interface IOrderRepository
	{
		Task<IList<Order>> GetAllOrderTypeAsync();
		Task<Order> GetOrderByKeyAsync(string key);
		Task<IList<Order>> SearchOrderByNameAsync(OrderSearchRequest request);
		Task<bool> DeleteOrderAsync(Order existedOrder);
		Task<bool> CreateOrderAsync(Order order);
	}
	public class OrderRepository /*IOrderRepository*/
	{
		private readonly SpayDBContext _context;

		public OrderRepository(SpayDBContext context)
		{
			_context = context;
		}

		//public async Task<bool> CreateOrderAsync(Order order)
		//{
		//	_context.Orders.Add(order);
		//	return await _context.SaveChangesAsync() > 0;
		//}

		//public async Task<bool> DeleteOrderAsync(Order existedOrder)
		//{
		//	existedOrder.Status = (byte)CardStatusEnum.Deleted;
		//	return await _context.SaveChangesAsync() > 0;
		//}

		//public async Task<IList<Order>> GetAllOrderTypeAsync()
		//{
		//	var orders = await _context.Orders
		//		.Include(o => o.CardKeyNavigation)
		//		.Include(o => o.CustomerKeyNavigation.UserKeyNavigation)
		//		.Include(o => o.StoreKeyNavigation.UserKeyNavigation)
		//		.Where(o => o.Status != (byte)OrderStatusEnum.Deleted)
		//		.ToListAsync();
		//	return orders; 
		//}

		//public async Task<Order> GetOrderByKeyAsync(string key)
		//{
		//	var orders = await _context.Orders
		//		.Include(o => o.CardKeyNavigation)
		//		.Include(o => o.CustomerKeyNavigation.UserKeyNavigation)
		//		.Include(o => o.StoreKeyNavigation.UserKeyNavigation)
		//		.Where(o => o.Status != (byte)OrderStatusEnum.Deleted)
		//		.FirstOrDefaultAsync(o => o.OrderKey.Equals(key));
		//	return orders;
		//}

		//public async Task<IList<Order>> SearchOrderByNameAsync(OrderSearchRequest request)
		//{
		//	var orders = await _context.Orders
		//		.Include(o => o.CardKeyNavigation)
		//		.Include(o => o.CustomerKeyNavigation.UserKeyNavigation)
		//		.Include(o => o.StoreKeyNavigation.UserKeyNavigation)
		//		.Where(o => o.CardKeyNavigation.CardName.Contains(request.CardName)
		//		&& o.CustomerKeyNavigation.UserKeyNavigation.Fullname.Contains(request.FromCustomer)
		//		&& o.StoreKeyNavigation.Name.Contains(request.CardName)
		//		&& o.Status != (byte)OrderStatusEnum.Deleted)
		//		.ToListAsync();
		//	return orders;
		//}
	}
}
