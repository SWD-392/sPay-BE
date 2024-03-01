using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Customer.RespondModel;

namespace SPay.Service.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile() { 
            CreateMap<GetAllCustomerResponse, Customer>().ReverseMap();
        }
    }
}
