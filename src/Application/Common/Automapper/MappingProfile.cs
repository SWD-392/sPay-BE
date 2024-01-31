using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Customer.Commands.CreateOrUpdateCustomerCommand;
using Application.Customer.Queries;
using AutoMapper;

namespace Application.Common.Automapper
{
    public class MappingProfile : Profile
    {
 

        public MappingProfile()
        {
            CreateMap<Models.Customer, CustomersResponseQueryDto>();
        }
    }
}
