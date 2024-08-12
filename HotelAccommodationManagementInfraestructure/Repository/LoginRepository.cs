using HotelAccommodationManagementDomain.Entities;
using HotelAccommodationManagementDomain.Repositories;
using HotelAccommodationManagementInfrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelAccommodationManagementInfrastructure.Repository
{
    public class LoginRepository : ILoginUserRepository
    {
        private readonly DataDbContext _context;

        public LoginRepository(DataDbContext context)
        {
            _context = context;
        }

        public async Task<bool> LoginUser(Login login)
        {
            try
            {
                var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Email == login.Email);
                if (existingUser == null || login.Password != existingUser.Password)
                {
                    throw new TaskCanceledException("Las credenciales no coinciden");
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
