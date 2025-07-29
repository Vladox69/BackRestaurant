using BackRestaurant.Models;

namespace BackRestaurant.Repository
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategories();
    }
}
