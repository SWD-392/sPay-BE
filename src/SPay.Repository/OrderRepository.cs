using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Order.Request;
using SPay.Repository.Enum;
using SPay.Repository.ResponseDTO;

namespace SPay.Repository
{
	public interface IOrderRepository
	{
		Task<IList<Order>> GetAllOrderTypeAsync();
		Task<Order> GetOrderByKeyAsync(string key);
		Task<bool> DeleteOrderAsync(Order existedOrder);
		Task<bool> CreateOrderAsync(Order order);
		Task<bool> ProcessMoney(MembershipResponseDTO item, CreateOrderRequest request);
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

		public async Task<bool> ProcessMoney(MembershipResponseDTO item, CreateOrderRequest request)
		{
			using (var transaction = await _context.Database.BeginTransactionAsync())
			{
				try
				{
					// Trừ tiền từ tài khoản thành viên
					item.Wallet.Balance -= request.TotalAmount;

					// Lưu thay đổi vào cơ sở dữ liệu
					await _context.SaveChangesAsync();

					// Tìm cửa hàng và cập nhật số tiền
					var store = await _context.Stores.SingleOrDefaultAsync(x => x.StoreKey.Equals(request.StoreKey));
					if (store == null)
					{
						throw new Exception("Error when retrieving store information");
					}
					store.WalletKeyNavigation.Balance += request.TotalAmount;

					// Lưu thay đổi vào cơ sở dữ liệu
					await _context.SaveChangesAsync();

					// Kết thúc transaction và lưu thay đổi
					await transaction.CommitAsync();

					// Nếu không có lỗi, trả về true
					return true;
				}
				catch (Exception ex)
				{
					// Nếu có lỗi, rollback transaction
					await transaction.RollbackAsync();

					// Log lỗi hoặc xử lý theo nhu cầu

					// Trả về false để thông báo rằng có lỗi xảy ra
					return false;
				}
			}
		}

	}
}
