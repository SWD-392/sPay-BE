using SPay.BO.RerferenceSRC.DTOs.Request.Category;
using SPay.BO.RerferenceSRC.DTOs.Response.Category;
using SPay.BO.RerferenceSRC.Paginate;
using SPay.DAO.ReferenceSRC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.Repository.ReferenceSRC
{
    public interface ICategoriesRepository
    {
        Task<IPaginate<GetCategoryResponse>> GetAllCategories(int page, int size);
        void CreateCategory(CreateCategoryRequest createCategory);
        Task<UpdateCategoryResponse> UpdateCategory(int categoryId, UpdateCategoryRequest request);

    }

    public class CategoriesRepository : ICategoriesRepository
    {
        public async void CreateCategory(CreateCategoryRequest request)
        {
            CategoryDAO.Instance.CreateCategory(request);
        }

        public async Task<IPaginate<GetCategoryResponse>> GetAllCategories(int page, int size)
        {
            return await CategoryDAO.Instance.GetAllCategories(page, size);
        }

        public async Task<UpdateCategoryResponse> UpdateCategory(int categoryId, UpdateCategoryRequest request)
        {
            return await CategoryDAO.Instance.UpdateCategory(categoryId, request);
        }
    }
}
