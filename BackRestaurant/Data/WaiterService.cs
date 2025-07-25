using BackRestaurant.Models;
using Microsoft.EntityFrameworkCore;

namespace BackRestaurant.Data
{
    public class WaiterService : IWaiterService
    {
        private readonly MyContext _context;
        private readonly IConfiguration _configuration;

        public WaiterService(MyContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Waiter>> GetAllWaiters()
        {
            try
            {
                return await _context.Waiters
                    .Include(w => w.user)
                    .Include(w=>w.business)
                    .Select(w => new Waiter
                    {
                        id = w.id,
                        shift = w.shift,
                        business_id = w.business_id,
                        user_id = w.user_id,
                        user = w.user,
                        business = new Business
                        {
                            id = w.business.id,
                            location = w.business.location,
                            email = w.business.email,
                            name = w.business.name,
                            phone = w.business.phone,
                            user_id = w.business.user_id
                        }
                    })
                    .ToListAsync();
            }catch (Exception ex) {
                throw new Exception($"An error occurred while fetching waiters {ex.Message}");
            }
        }

        public async Task<Waiter> GetWaiterById(int id)
        {
            try
            {
                Waiter? waiterFind = await _context.Waiters.FirstOrDefaultAsync(w=> w.id == id);
                return waiterFind ?? throw new Exception($"No waiter found");
            }
            catch(Exception ex)
            {
                throw new Exception($"An error occurred while fetching waiter by id {ex.Message}");
            }
        }

        public async Task<bool> InsertWaiter(Waiter waiter)
        {
            try
            {
                Waiter? waiterFind=await _context.Waiters.FirstOrDefaultAsync(w=>(w.user_id == waiter.user_id&& w.business_id == waiter.business_id));
                if (waiterFind!=null)
                {
                    throw new Exception($"Waiter already exist");
                }
                await _context.Waiters.AddAsync(waiter);
                return await _context.SaveChangesAsync() > 0;
            }catch (Exception ex) {
                throw new Exception($"An error occurred while insert waiter {ex.Message}");
            }
        }

        public async Task<bool> SaveWaiter(Waiter waiter)
        {
            try
            {
                if (waiter.id>0){
                    return await UpdateWaiter(waiter);
                }else{
                    return await InsertWaiter(waiter);
                }
            }catch(Exception ex){
                throw new Exception($"An error occurred while saving waiter {ex.Message}");
            }
        }

        public async Task<bool> UpdateWaiter(Waiter waiter)
        {
            try
            {
                _context.Entry(waiter).State = EntityState.Modified;
                return await _context.SaveChangesAsync() > 0;
            }
            catch(Exception ex){
                throw new Exception($"An error occurred while update waiter {ex.Message}");
            }
        }
    }
}
