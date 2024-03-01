using SPay.BO.RerferenceSRC.DTOs.Request.Category;
using SPay.BO.RerferenceSRC.DTOs.Response.Category;
using SPay.BO.RerferenceSRC.Paginate;
using SPay.Repository.ReferenceSRC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.Service.ReferenceSRC
{
    public interface ICategoryService
    {
        Task<IPaginate<GetCategoryResponse>> GetAllCategories(int page, int size);
        void CreateCategory(CreateCategoryRequest request);
        Task<UpdateCategoryResponse> UpdateCategory(int categoryId, UpdateCategoryRequest request);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoriesRepository _categoryRepository;

        public CategoryService()
        {
            if (_categoryRepository == null)
            {
                _categoryRepository = new CategoriesRepository();
            }
        }

        public async void CreateCategory(CreateCategoryRequest request)
        {
            _categoryRepository.CreateCategory(request);
        }

        public async Task<IPaginate<GetCategoryResponse>> GetAllCategories(int page, int size)
        {
            return await _categoryRepository.GetAllCategories(page, size);
        }

        public async Task<UpdateCategoryResponse> UpdateCategory(int categoryId, UpdateCategoryRequest request)
        {
            return await _categoryRepository.UpdateCategory(categoryId, request);
        }
    }
}
