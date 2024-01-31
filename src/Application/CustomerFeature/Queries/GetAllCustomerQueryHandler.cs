// GetAllCustomerQueryHandler.cs
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Customer.Queries
{
    public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, List<CustomersResponseQueryDto>>
    {
        private readonly ExampleDbContext _context;
        private readonly IMapper _mapper;
        public GetAllCustomerQueryHandler(ExampleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CustomersResponseQueryDto>> Handle(GetAllCustomerQuery query, CancellationToken cancellationToken)
        {
            var customerList = await _context.Customers.ToListAsync();
            // Kiểm tra null trước khi ánh xạ để tránh lỗi NullReferenceException
            var customerRes = _mapper.Map<List<CustomersResponseQueryDto>>(customerList);
            return customerRes;
        }
    }
}
