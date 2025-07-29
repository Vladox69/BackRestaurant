using BackRestaurant.Models;

namespace BackRestaurant.Repository
{
    public interface ITableService
    {
        Task<IEnumerable<Table>> GetTablesByBusinessId(int id);
    }
}
