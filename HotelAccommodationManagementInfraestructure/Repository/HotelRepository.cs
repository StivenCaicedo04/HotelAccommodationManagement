using HotelAccommodationManagementDomain.Entities;
using HotelAccommodationManagementInfrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelAccommodationManagementDomain.Repositories.Repository
{
    public class HotelRepository : IHotelRepository
    {
        private readonly DataDbContext _context;

        public HotelRepository(DataDbContext context)
        {
            _context = context;
        }

        public async Task<Hotels> GetHotelById(int id)
        {
            try
            {
                return await _context.Hotels.FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<Hotels> AddHotel(Hotels hotel)
        {
            try
            {
                _context.Hotels.Add(hotel);
                await _context.SaveChangesAsync();
                return _context.Hotels
                       .SingleOrDefault(h => h.Id == hotel.Id);
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public async Task<bool> UpdateHotel(Hotels hotel)
        {
            try
            {
                var existingHotel = await _context.Hotels.FindAsync(hotel.Id);

                if (existingHotel == null)
                {
                    return false; 
                }

                existingHotel.Name = hotel.Name;
                existingHotel.Address = hotel.Address;
                existingHotel.IsEnabled = hotel.IsEnabled;
                existingHotel.ModifiedAt = DateTime.UtcNow; 

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<bool> DeleteHotel(int id)
        {
            try
            {
                var hotel = await GetHotelById(id);
                if (hotel != null)
                {
                    _context.Hotels.Remove(hotel);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else return false;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<List<Hotels>> GetAllHotels()
        {
            try
            {
                var result = await _context.Hotels.ToListAsync();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
