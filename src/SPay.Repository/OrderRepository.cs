using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPay.BO.DataBase.Models;
using SPay.Repository.Enum;

namespace SPay.Repository
{
	public interface IOrderRepository
	{
		Task<IList<Order>> GetAllOrderTypeAsync();
		Task<Order> GetOrderByKeyAsync(string key);
		Task<bool> DeleteOrderAsync(Order existedOrder);
		Task<bool> CreateOrderAsync(Order order);
	}
	public class OrderRepository : IOrderRepository
	{
		private readonly SpayDBContext _context;

		public OrderRepository(SpayDBContext context)
		{
			_context = context;
		}

		public async Task<bool> CreateOrderAsync(Order order)
		{
			_context.Orders.Add(order);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> DeleteOrderAsync(Order existedOrder)
		{
			existedOrder.Status = (byte)OrderStatusEnum.Deleted;
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<IList<Order>> GetAllOrderTypeAsync()
		{
			var orders = await _context.Orders
				.Include(o => o.StoreKeyNavigation)
				.Include(o => o.StoreKeyNavigation.StoreCateKeyNavigation)
				.Include(o => o.MembershipKeyNavigation)
				.Where(o => o.Status != (byte)OrderStatusEnum.Deleted)
				.ToListAsync();
			return orders;
		}

		public async Task<Order> GetOrderByKeyAsync(string key)
		{
			var orders = await _context.Orders
				.Where(o => o.Status != (byte)OrderStatusEnum.Deleted)
				.Include(o => o.StoreKeyNavigation)
				.Include(o => o.StoreKeyNavigation.StoreCateKeyNavigation)
				.Include(o => o.MembershipKeyNavigation)
				.FirstOrDefaultAsync(o => o.OrderKey.Equals(key));
			if(orders == null)
			{
				throw new Exception($"Order with {key} not found.");
			}
			return orders;
		}
	}
}
