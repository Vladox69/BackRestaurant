using BackRestaurant.Models;

namespace BackRestaurant.Repository
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<bool> InserProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> SaveProduct(Product product);
        Task<IEnumerable<Product>> GetProductsByBusinessId(int id);
    }
}
