using TicketStationMVC.Data.Entities;
using TicketStationMVC.Repositories;
using TicketStationMVC.Services.ServiceInterfaces;
using TicketStationMVC.ViewModels.Category;

namespace TicketStationMVC.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        public CategoryService(IRepository<Category> repository)
        {
            _categoryRepository = repository;
        }
        public async Task<Category> CreateAsync(CategoryVM categoryVM)
        {
            Category category = new Category()
            {
                Name = categoryVM.Name,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            return await _categoryRepository.CreateAsync(category);
        }

        public async Task<Category> DeleteAsync(int categoryId)
        {
            return await _categoryRepository.DeleteAsync(categoryId);
        }

        public async Task<ICollection<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _categoryRepository.GetByIdAsync(categoryId);
        }

        public async Task<Category> UpdateAsync(CategoryVM categoryVM)
        {
            Category category = await this.GetCategoryByIdAsync(categoryVM.Id);
            category.Name = categoryVM.Name;
            category.ModifiedAt = DateTime.Now;
            return await _categoryRepository.UpdateAsync(category);
        }
    }
}
