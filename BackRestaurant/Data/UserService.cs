using BackRestaurant.Models;
using BackRestaurant.Repository;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackRestaurant.Data
{
    public class UserService : IUserService
    {
        private readonly MyContext _context;
        private readonly IConfiguration _configuration;
        public UserService(MyContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception("An error occurred while fetching users", ex);
            }
        }

        public async Task<bool> InsertUser(User user)
        {
            try
            {
                User? userFind = await _context.Users.FirstOrDefaultAsync(u => u.email == user.email);
                if (userFind != null)
                {
                    throw new Exception("User already exists");
                }
                user.password = BCrypt.Net.BCrypt.HashPassword(user.password);
                await _context.Users.AddAsync(user);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<string> Login(LoginForm form)
        {
            try
            {
                User? user = await _context.Users.FirstOrDefaultAsync(u=>u.email== form.email) ?? throw new Exception("User not found");
                if (!BCrypt.Net.BCrypt.Verify(form.password,user.password))
                {
                    throw new Exception("Invalid credentials");
                }
                Business? business = await _context.Business.FirstOrDefaultAsync(b => b.user_id == user.boss_id) ?? throw new Exception("User not found");
                return GenerateJwtToken(user, business);
            }catch (Exception ex) {
                throw new Exception($"{ex.Message}" );
            }
        }

        public async Task<bool> SaveUser(User user)
        {
            try
            {
                if (user.id>0)
                {
                    return await UpdateUser(user);
                }
                else
                {
                    return await InsertUser(user);
                }
            }catch (Exception ex) {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<bool> UpdateUser(User user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex) {
                throw new Exception($"{ex.Message}");
            }
        }

        private string GenerateJwtToken(User user,Business business)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.id.ToString()), 
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 
            new Claim("role", user.role),
            new Claim("name", user.name),
            new Claim("boss_id", user.boss_id.ToString()),
            new Claim("business_id", business.id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"], 
                audience: _configuration["Jwt:Audience"], 
                claims: claims,
                expires: DateTime.Now.AddHours(1), 
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
