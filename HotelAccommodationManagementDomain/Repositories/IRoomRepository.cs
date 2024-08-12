using HotelAccommodationManagementDomain.Entities;

namespace HotelAccommodationManagementDomain.Repositories
{
    public interface IRoomRepository
    {
        Task<Rooms> GetRoomById(int id);
        Task<List<Rooms>> GetRoomsByHotel(int idHotel);
        Task<Rooms> AddRoom(Rooms room);
        Task<bool> UpdateRoom(Rooms room);
        Task<bool> DeleteRoom(int id);
        Task<List<Rooms>> GetAllRooms();
    }
}
