using HotelAccommodationManagementDomain.Entities;

namespace HotelAccommodationManagementDomain.Repositories
{
    public interface IUserRepository
    {
        Task<Users> GetUserById(int id);
        Task<Users> AddUser(Users user);
        Task<bool> UpdateUser(Users user);
        Task<bool> DeleteUser(int id);
        Task<List<Users>> GetAllUsers();
    }
}
