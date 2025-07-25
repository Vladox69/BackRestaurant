using BackRestaurant.Models;

namespace BackRestaurant.Data
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersByIdBusiness(int id);
        Task<IEnumerable<Order>> GetOrdersByIdWaiterAndStatus(int id,string status);
        Task<Order> GetOrderById(int id);
        Task<bool> InsertOrder(Order order);
        Task<bool> UpdateOrder(Order order);
        Task<bool> SaveOrder(Order order);
        Task<Order> CreateOrderTransaction(OrderBody body);
    }
}
