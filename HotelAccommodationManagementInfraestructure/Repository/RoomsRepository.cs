using HotelAccommodationManagementDomain.Entities;
using HotelAccommodationManagementInfrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelAccommodationManagementDomain.Repositories.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly DataDbContext _context;

        public RoomRepository(DataDbContext context)
        {
            _context = context;
        }

        public async Task<Rooms> GetRoomById(int id)
        {
            try
            {
                return await _context.Rooms.FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public async Task<List<Rooms>> GetRoomsByHotel(int idHotel)
        {
            try
            {
                return await _context.Rooms.Where(r => r.HotelId == idHotel).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public async Task<Rooms> AddRoom(Rooms room)
        {
            try
            {
                _context.Rooms.Add(room);
                await _context.SaveChangesAsync();
                return _context.Rooms
                               .SingleOrDefault(h => h.Id == room.Id);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<bool> UpdateRoom(Rooms room)
        {
            try
            {
                var existingRoom = await _context.Rooms.FindAsync(room.Id);

                if (existingRoom == null)
                {
                    return false;
                }

                existingRoom.HotelId = room.HotelId;
                existingRoom.RoomType = room.RoomType;
                existingRoom.BaseCost = room.BaseCost;
                existingRoom.Taxes = room.Taxes;
                existingRoom.Location = room.Location;
                existingRoom.IsEnabled = room.IsEnabled;
                existingRoom.ModifiedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public async Task<bool> DeleteRoom(int id)
        {
            try
            {
                var room = await GetRoomById(id);
                if (room != null)
                {
                    _context.Rooms.Remove(room);
                    await _context.SaveChangesAsync();
                    return true;
                }else return false;
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public async Task<List<Rooms>> GetAllRooms()
        {
            try
            {
                return await _context.Rooms.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
