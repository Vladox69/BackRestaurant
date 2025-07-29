using BackRestaurant.Models;
using BackRestaurant.Repository;
using Microsoft.EntityFrameworkCore;

namespace BackRestaurant.Data
{
    public class TableService : ITableService
    {
        private readonly MyContext _context;
        private readonly IConfiguration _configuration;

        public TableService(MyContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<IEnumerable<Table>> GetTablesByBusinessId(int id)
        {
            try
            {
                return await _context.Tables.Where(t=>t.business_id == id).ToListAsync();
            }catch(Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
