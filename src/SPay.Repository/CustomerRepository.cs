using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPay.BO.DataBase.Models;
using SPay.Repository.Enum;
using static System.Formats.Asn1.AsnWriter;

namespace SPay.Repository
{
    public interface ICustomerRepository
    {
        Task<IList<Customer>> GetAllCustomerAsync();
		Task<Customer> GetCustomerByKeyAsync(string key);
		Task<IList<Customer>> SearchCustomerByNameAsync(string keyWord);
		Task<bool> DeleteCustomerAsync(Customer existedCus);
		Task<bool> CreateCustomerAsync(Customer customer);
	}
	public class CustomerRepository : ICustomerRepository
    {
        private readonly SPayDbContext _context;
        public CustomerRepository(SPayDbContext _context)
        {
            this._context = _context;
        }

		public async Task<bool> CreateCustomerAsync(Customer customer)
		{
			_context.Customers.Add(customer);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> DeleteCustomerAsync(Customer existedCus)
		{
			var cusUser = await _context.Users.FirstOrDefaultAsync(u => u.UserKey.Equals(existedCus.UserKey));
			cusUser.Status = (byte)UserStatusEnum.Deleted;
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<IList<Customer>> GetAllCustomerAsync()
        {
            return await _context.Customers
				.Where(c => c.UserKeyNavigation.Status == (byte)UserStatusEnum.Active
				&& c.UserKeyNavigation.Role == (byte)RoleEnum.Customer)
				.Include(c => c.UserKeyNavigation)
				.ToListAsync();
        }

		public async Task<Customer> GetCustomerByKeyAsync(string key)
		{
			return await _context.Customers
			   .Include(c => c.UserKeyNavigation)
			   .FirstOrDefaultAsync(c => c.CustomerKey.Equals(key) 
			   && c.UserKeyNavigation.Status == (byte)UserStatusEnum.Active
			   && c.UserKeyNavigation.Role == (byte)RoleEnum.Customer);
		}

		public async Task<IList<Customer>> SearchCustomerByNameAsync(string keyWord)
		{
			return await _context.Customers
				.Where(c => c.UserKeyNavigation.Fullname.ToLower().Contains(keyWord.ToLower())
									&& c.UserKeyNavigation.Status == (byte)UserStatusEnum.Active
									&& c.UserKeyNavigation.Role == (byte)RoleEnum.Customer)
				.Include(c => c.UserKeyNavigation)
				.ToListAsync();
		}
	}
}
