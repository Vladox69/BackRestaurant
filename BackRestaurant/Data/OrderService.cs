using BackRestaurant.Models;
using BackRestaurant.Repository;
using Microsoft.EntityFrameworkCore;

namespace BackRestaurant.Data
{
    public class OrderService : IOrderService
    {
        private readonly MyContext _context;
        private readonly IConfiguration _configuration;
        private readonly OrderNotificationService _orderNotification;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderService(MyContext context, IConfiguration configuration, OrderNotificationService orderNotification, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _orderNotification = orderNotification;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Order> CreateOrderTransaction(OrderBody body)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        await _context.Orders.AddAsync(body.order);
                        int res = await _context.SaveChangesAsync();
                        if (res <= 0)
                        {
                            throw new Exception($"An error occurred while creating the order transaction");
                        }

                        if (!body.order.id.HasValue)
                        {
                            throw new Exception("Order ID is null after saving the order.");
                        }

                        foreach (var item in body.items)
                        {
                            item.order_id = body.order.id.Value; // Fix: Ensure id is not null before accessing Value
                            await _context.OrderItems.AddAsync(item);
                        }
                        res = await _context.SaveChangesAsync();

                        if (res <= 0)
                        {
                            throw new Exception($"An error occurred while creating the order transaction");
                        }
                        await transaction.CommitAsync();
                        var boss_id = _httpContextAccessor.HttpContext.User?.FindFirst("business_id")?.Value;
                        await _orderNotification.NotifyNewOrderToKitchen(body.order,boss_id);
                        return body.order;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"{ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Order> GetOrderById(int id)
        {
            try
            {
                Order? orderFind = await _context.Orders.FirstOrDefaultAsync(o=>o.id == id);
                return orderFind ?? throw new Exception($"No order found");
            }
            catch(Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByIdBusiness(int id)
        {
            try
            {
                return await _context.Orders
                    .Where(p => p.table.business_id == id)
                    .Select(p => new Order
                    {
                        id = p.id,
                        status = p.status,
                        total = p.total,
                        waiter_id = p.waiter_id,
                        table_id = p.table_id,
                        waiter = new Waiter
                        {
                            id = p.waiter.id,
                            shift = p.waiter.shift,
                            user_id = p.waiter.user_id,
                        },
                        table = new Table
                        {
                            id = p.table.id,
                            business_id = p.table.business_id,
                            capacity = p.table.capacity,
                            location = p.table.location,
                            table_number = p.table.table_number
                        }
                    })
                    .OrderByDescending(p => p.id)
                    .ToListAsync();
            }
            catch(Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByIdWaiterAndStatus(int id,string status)
        {
            try
            {
                return await _context.Orders
                    .Where(p=>(string.IsNullOrEmpty(status) || p.status == status) && p.waiter_id==id)
                    .Select(p => new Order
                    {
                        id = p.id,
                        status = p.status,
                        total = p.total,
                        waiter_id = p.waiter_id,
                        table_id = p.table_id,
                        waiter = new Waiter
                        {
                            id = p.waiter.id,
                            shift = p.waiter.shift,
                            user_id = p.waiter.user_id,
                        },
                        table = new Table
                        {
                            id = p.table.id,
                            business_id = p.table.business_id,
                            capacity = p.table.capacity,
                            location = p.table.location,
                            table_number = p.table.table_number
                        }
                    }).ToListAsync();
            }
            catch(Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public Task<bool> InsertOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
