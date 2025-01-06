using TicketStationMVC.Data.Entities;
using TicketStationMVC.ViewModels.Category;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TicketStationMVC.Services.ServiceInterfaces
{
    public interface ICategoryService
    {
        public Task<Category> GetCategoryByIdAsync(int categoryId);
        public Task<ICollection<Category>> GetAllCategoriesAsync();
        public Task<Category> CreateAsync(Category category);
        public Task<Category> UpdateAsync(Category category);
        public Task<Category> DeleteAsync(int categoryId);


    }
}
