using BackRestaurant.Models;

namespace BackRestaurant.Data
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategories();
    }
}
