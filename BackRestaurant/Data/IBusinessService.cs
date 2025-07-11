using BackRestaurant.Models;

namespace BackRestaurant.Data
{
    public interface IBusinessService
    {
        Task<IEnumerable<Business>> GetAllBusiness();
        Task<bool> InsertBusiness(Business business);
        Task<bool> UpdateBusiness(Business business);
        Task<bool> SaveBusiness(Business business);
    }
}
