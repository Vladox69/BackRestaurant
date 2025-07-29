using BackRestaurant.Models;
using BackRestaurant.Repository;
using Microsoft.EntityFrameworkCore;

namespace BackRestaurant.Data
{
    public class OrderItemService : IOrderItemService
    {
        private readonly MyContext _context;
        private readonly IConfiguration _configuration;
        private readonly OrderNotificationService _orderNotification;
        public OrderItemService(MyContext context, IConfiguration configuration, OrderNotificationService orderNotification)
        {
            _context = context;
            _configuration = configuration;
            _orderNotification = orderNotification;
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

        public Task<OrderItem> InsertOrderItem(OrderItem orderItem)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderItem> SaveOrderItem(OrderItem orderItem)
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

        public async Task<OrderItem> UpdateOrderItem(OrderItem orderItem)
        {
            try
            {
                _context.Entry(orderItem).State = EntityState.Modified;
                Order? order = await _context.Orders.FirstOrDefaultAsync(o=>o.id==orderItem.order_id) ?? throw new Exception($"No order found");
                if (orderItem.status != "delivered") { 
                    await _orderNotification.NotifyOrderReadyToWaiter(orderItem, order.waiter_id.ToString());
                }
                var res = await _context.SaveChangesAsync() > 0;
                if (!res)
                {
                    throw new Exception("Updated failed");
                }
                return orderItem;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
