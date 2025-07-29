using BackRestaurant.Models;
using BackRestaurant.Repository;
using Microsoft.EntityFrameworkCore;

namespace BackRestaurant.Data
{
    public class BusinessService : IBusinessService
    {
        private readonly MyContext _context;
        private readonly IConfiguration _configuration;

        public BusinessService(MyContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Business>> GetAllBusiness()
        {
            try
            {
                return await _context.Business.Include(b=>b.user).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Business> GetBusinessByUserId(int id)
        {
            try
            {
                Business? businessFind = await _context.Business.FirstOrDefaultAsync(b => b.user_id == id);
                return businessFind ?? throw new Exception($"No business found");
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<bool> InsertBusiness(Business business)
        {
            try
            {
                Business? businessFind = await _context.Business.FirstOrDefaultAsync(b => b.name == business.name);
                if (businessFind != null)
                {
                    throw new Exception("Business is already exist");
                }
                await _context.Business.AddAsync(business);
                return await _context.SaveChangesAsync()>0;
            }catch (Exception ex) {
                throw new Exception($"{ex.Message}");
            }

        }

        public async Task<bool> SaveBusiness(Business business)
        {
            try
            {
                if (business.id>0)
                {
                    return await UpdateBusiness(business);
                }
                else
                {
                    return await InsertBusiness(business);
                }
            }catch(Exception ex){
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<bool> UpdateBusiness(Business business)
        {
            try
            {
                _context.Entry(business).State = EntityState.Modified;
                return await _context.SaveChangesAsync()>0;
            }catch(Exception ex){
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
