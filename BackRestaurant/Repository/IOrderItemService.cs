using BackRestaurant.Models;

namespace BackRestaurant.Repository
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItem>> GetOrderItemsByOrderId(int id);
        Task<OrderItem> GetOrderItemById(int id);
        Task<bool> InsertOrderItem(OrderItem orderItem);
        Task<bool> UpdateOrderItem(OrderItem orderItem);
        Task<bool> SaveOrderItem(OrderItem orderItem);
    }
}
