using BackRestaurant.Models;
using Microsoft.EntityFrameworkCore;

namespace BackRestaurant.Data
{
    public class ProductService : IProductService
    {
        private readonly MyContext _context;
        private readonly IConfiguration _configuration;

        public ProductService(MyContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                return await _context.Products
                    .Select(p => new Product 
                    { 
                        id=p.id,
                        business_id=p.business_id,
                        category_id=p.category_id,
                        description=p.description,
                        name= p.name,
                        price= p.price
                    })
                    .ToListAsync();
            }catch (Exception ex) {
                throw new Exception($"An error occurred while fetching products {ex.Message}");
            }
        }

        public async Task<IEnumerable<Product>> GetProductsByBusinessId(int id)
        {
            try
            {
                return await _context.Products
                    .Where(p => p.business_id == id)
                    .Select(p => new Product
                    {
                        id = p.id,
                        business_id = p.business_id,
                        category_id = p.category_id,
                        description = p.description,
                        name = p.name,
                        price = p.price
                    })
                    .ToListAsync();
            }catch(Exception ex){
                throw new Exception($"An error occurred while fetching products {ex.Message}");
            }
        }

        public async Task<bool> InserProduct(Product product)
        {
            try
            {
                Product? productFind = await _context.Products
                    .FirstOrDefaultAsync(w => (w.name==product.name&&w.category_id==product.category_id));
                if (productFind != null)
                {
                    throw new Exception($"Product already exist");
                }
                await _context.Products.AddAsync(product);
                return await _context.SaveChangesAsync() > 0;
            }catch(Exception ex){
                throw new Exception($"An error occurred while insert product {ex.Message}");
            }
        }

        public async Task<bool> SaveProduct(Product product)
        {
            try
            {
                if (product.id>0)
                {
                    return await UpdateProduct(product);
                }
                else
                {
                    return await InserProduct(product);
                }
            }catch(Exception ex)
            {
                throw new Exception($"An error occurred while saving product {ex.Message}");
            }
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            try
            {
                _context.Entry(product).State = EntityState.Modified;
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while update product {ex.Message}");
            }
        }
    }
}
