using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Customer.Commands.CreateOrUpdateCustomerCommand;
using Application.Models;
using AutoMapper;
using MediatR;

namespace Application.Services
{
    public interface ICustomerService
    {
        Task<Unit> CreateOrUpdateCustomerAsync(CreateOrUpdateCustomerCommand command);
        Task<List<Application.Models.Customer>> GetAllCustomer();
    }

    public class CustomerService : ICustomerService
    {
        private readonly ExampleDbContext _context;
        private readonly IMapper _mapper;
        public CustomerService(ExampleDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }
        public async Task<Unit> CreateOrUpdateCustomerAsync(CreateOrUpdateCustomerCommand command)
        {
            var customerInfo = _mapper.Map<Models.Customer>(command);
            var id = customerInfo.CustomerId;

            if (id != null)
            {
                // Case: Update an existing customer
                var existingCustomer = await _context.Customers.FindAsync(id);

                if (existingCustomer != null)
                {
                    // Update existing customer properties
                    existingCustomer = customerInfo;

                    await _context.SaveChangesAsync();

                    return Unit.Value; // Operation succeeded
                }
                // Handle the case where the customer with the provided id is not found
                throw new InvalidOperationException($"Customer with id {id} not found");
            }
            else
            {
                // Case: Create a new customer
                _context.Customers.Add(customerInfo);
                await _context.SaveChangesAsync();

                return Unit.Value; // Operation succeeded
            }
        }

        public async Task<List<Application.Models.Customer>> GetAllCustomer()
        {
            return await _context.Customers.ToListAsync();
        }
    }
}
