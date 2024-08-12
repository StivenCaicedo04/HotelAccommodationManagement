using HotelAccommodationManagementDomain.Entities;

namespace HotelAccommodationManagementDomain.Repositories
{
    public interface ILoginUserRepository
    {
        Task<bool> LoginUser(Login login);
    }
}
