using AutoMapper;
using SPay.BO.ReferenceSRC.Models;
using SPay.BO.RerferenceSRC.DTOs.Request.Category;
using SPay.BO.RerferenceSRC.DTOs.Response.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.DAO.ReferenceSRC.Mappers
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<CreateCategoryRequest, Category>();
            CreateMap<Category, UpdateAccountResponse>();
        }
    }
}
