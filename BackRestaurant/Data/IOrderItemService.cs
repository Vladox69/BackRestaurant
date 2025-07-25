using BackRestaurant.Models;

namespace BackRestaurant.Data
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItems>> GetOrderItemsByIdOrder(int id);
        Task<OrderItems> GetOrderItemById(int id);
        Task<bool> InsertOrderItem(OrderItems orderItem);
        Task<bool> UpdateOrderItem(OrderItems orderItem);
        Task<bool> SaveOrderItem(OrderItems orderItem);
    }
}
