using BackRestaurant.Models;
using BackRestaurant.Repository;
using Microsoft.EntityFrameworkCore;

namespace BackRestaurant.Data
{
    public class OrderItemService : IOrderItemService
    {
        private readonly MyContext _context;
        private readonly IConfiguration _configuration;

        public OrderItemService(MyContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public Task<OrderItem> GetOrderItemById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderId(int id)
        {
            try
            {
                return await _context
                    .OrderItems
                    .Where(oi => oi.order_id == id)
                    .ToListAsync();
            }catch(Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public Task<bool> InsertOrderItem(OrderItem orderItem)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveOrderItem(OrderItem orderItem)
        {
            try
            {
                if (orderItem.id > 0)
                {
                    return await UpdateOrderItem(orderItem);
                }
                else
                {
                    return await InsertOrderItem(orderItem);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<bool> UpdateOrderItem(OrderItem orderItem)
        {
            try
            {
                _context.Entry(orderItem).State = EntityState.Modified;
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
