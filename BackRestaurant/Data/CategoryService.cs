using BackRestaurant.Models;
using Microsoft.EntityFrameworkCore;

namespace BackRestaurant.Data
{
    public class CategoryService : ICategoryService
    {
        private readonly MyContext _context;
        private readonly IConfiguration _configuration;

        public CategoryService(MyContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            try
            {
                return await _context.Categories.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching products {ex.Message}");
            }
        }
    }
}
