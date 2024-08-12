using HotelAccommodationManagementDomain.Entities;

namespace HotelAccommodationManagementDomain.Repositories
{
    public interface IHotelRepository
    {
        Task<Hotels> GetHotelById(int id);
        Task<Hotels> AddHotel(Hotels hotel);
        Task<bool> UpdateHotel(Hotels hotel);
        Task<bool> DeleteHotel(int id);
        Task<List<Hotels>> GetAllHotels();
    }
}
