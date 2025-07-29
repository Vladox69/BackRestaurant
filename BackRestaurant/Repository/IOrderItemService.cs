using BackRestaurant.Models;

namespace BackRestaurant.Repository
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItem>> GetOrderItemsByOrderId(int id);
        Task<OrderItem> GetOrderItemById(int id);
        Task<OrderItem> InsertOrderItem(OrderItem orderItem);
        Task<OrderItem> UpdateOrderItem(OrderItem orderItem);
        Task<OrderItem> SaveOrderItem(OrderItem orderItem);
    }
}
