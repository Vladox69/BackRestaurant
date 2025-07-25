using BackRestaurant.Models;

namespace BackRestaurant.Data
{
    public interface IWaiterService
    {
        Task<IEnumerable<Waiter>> GetAllWaiters();
        Task<bool> InsertWaiter(Waiter waiter);
        Task<bool> UpdateWaiter(Waiter waiter);
        Task<bool> SaveWaiter(Waiter waiter);
        Task<Waiter> GetWaiterById(int id);
    }
}
