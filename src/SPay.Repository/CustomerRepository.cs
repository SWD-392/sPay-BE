using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Customer.RespondModel;

namespace SPay.Repository
{
    public interface ICustomerRepository
    {
        Task<IList<Customer>> GetAllCustomer();
    }
    public class CustomerRepository : ICustomerRepository
    {
        private readonly SPayDbContext _context;
        public CustomerRepository(SPayDbContext _context)
        {
            this._context = _context;
        }
        public async Task<IList<Customer>> GetAllCustomer()
        {
            return await _context.Customers.ToListAsync();
        }
    }
}
