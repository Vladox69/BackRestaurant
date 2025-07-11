using BackRestaurant.Models;

namespace BackRestaurant.Data
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<string> Login(LoginForm login);
        Task<bool> InsertUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> SaveUser(User user);
    }
}
