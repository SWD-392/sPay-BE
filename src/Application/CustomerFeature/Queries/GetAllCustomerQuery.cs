using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace Application.Customer.Queries
{
    public class GetAllCustomerQuery: IRequest<List<CustomersResponseQueryDto>>
    {
        public bool BypassCache => false;
    }
}
