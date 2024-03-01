using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SPay.BO.RerferenceSRC.DTOs.Request.Category;
using SPay.BO.RerferenceSRC.DTOs.Response.Category;
using SPay.BO.RerferenceSRC.Paginate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPay.DAO.ReferenceSRC.Mappers;
using SPay.BO.ReferenceSRC.Models;

namespace SPay.DAO.ReferenceSRC
{
    public class CategoryDAO
    {
        private readonly ITCenterContext _dbContext = null;
        private readonly IMapper _mapper = null;

        private static CategoryDAO instance;
        public static CategoryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoryDAO();
                }
                return instance;
            }
        }

        public CategoryDAO()
        {
            if (_dbContext == null)
            {
                _dbContext = new ITCenterContext();
            }
            if (_mapper == null)
            {
                _mapper = new Mapper(new MapperConfiguration(mc => mc.AddProfile(new CategoryMapper())).CreateMapper().ConfigurationProvider);
            }
        }

        public async Task<IPaginate<GetCategoryResponse>> GetAllCategories(int page, int size)
        {
            IPaginate<GetCategoryResponse> categoriesList = await _dbContext.Categories.Select(cate => new GetCategoryResponse
            {
                CategoryId = cate.CategoryId,
                CategoryName = cate.CategoryName,
                Description = cate.Description,
            }).ToPaginateAsync(page, size, 1);

            return categoriesList;
        }

        public async void CreateCategory(CreateCategoryRequest request)
        {
            _dbContext.Categories.Add(_mapper.Map<Category>(request));
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UpdateCategoryResponse> UpdateCategory(int categoryId, UpdateCategoryRequest request)
        {
            Category category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == categoryId);
            if (category != null)
            {
                category.CategoryName = request.CategoryName;
                category.Description = request.Description;
            }
            _dbContext.Update(category);
            await _dbContext.SaveChangesAsync();
            UpdateCategoryResponse response = _mapper.Map<UpdateCategoryResponse>(category);
            return response;
        }
    }
}
