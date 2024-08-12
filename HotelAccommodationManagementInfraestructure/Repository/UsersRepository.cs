using HotelAccommodationManagementDomain.Entities;
using HotelAccommodationManagementInfrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelAccommodationManagementDomain.Repositories.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataDbContext _context;

        public UserRepository(DataDbContext context)
        {
            _context = context;
        }

        public async Task<Users> GetUserById(int id)
        {
            try
            {
                return await _context.Users.FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<Users> AddUser(Users user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return _context.Users
                       .SingleOrDefault(h => h.Id == user.Id);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<bool> UpdateUser(Users user)
        {
            try
            {
                var existingUser = await _context.Users.FindAsync(user.Id);

                if (existingUser == null)
                {
                    return false;
                }

                existingUser.UserName = user.UserName;
                existingUser.Password = user.Password;
                existingUser.Email = user.Email;
                existingUser.Role = user.Role;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                var user = await GetUserById(id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                    return true;
                }else return false;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<List<Users>> GetAllUsers()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
