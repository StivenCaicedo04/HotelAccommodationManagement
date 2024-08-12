using HotelAccommodationManagementDomain.Entities;

namespace HotelAccommodationManagementDomain.Repositories
{
    public interface IPassengerRepository
    {
        Task<Passengers> GetPassengerById(int id);
        Task<Passengers> AddPassenger(Passengers passengers);
        Task<bool> UpdatePassenger(Passengers passengers);
        Task<bool> DeletePassenger(int id);
        Task<List<Passengers>> GetAllPassengers();
    }
}
