using HotelAccommodationManagementDomain.Entities;
using HotelAccommodationManagementInfrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelAccommodationManagementDomain.Repositories.Repository
{
    public class PassengerRepository : IPassengerRepository
    {
        private readonly DataDbContext _context;

        public PassengerRepository(DataDbContext context)
        {
            _context = context;
        }

        public async Task<Passengers> GetPassengerById(int id)
        {
            try
            {
                return await _context.Passengers.FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<Passengers> AddPassenger(Passengers passenger)
        {
            try
            {
                _context.Passengers.Add(passenger);
                await _context.SaveChangesAsync();
                return _context.Passengers
                       .SingleOrDefault(h => h.Id == passenger.Id);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<bool> UpdatePassenger(Passengers passenger)
        {
            try
            {
                var existingPassenger = await _context.Passengers.FindAsync(passenger.Id);

                if (existingPassenger == null)
                {
                    return false;
                }

                existingPassenger.ReservationId = passenger.ReservationId;
                existingPassenger.FirstName = passenger.FirstName;
                existingPassenger.LastName = passenger.LastName;
                existingPassenger.BirthDate = passenger.BirthDate;
                existingPassenger.Gender = passenger.Gender;
                existingPassenger.DocumentType = passenger.DocumentType;
                existingPassenger.DocumentNumber = passenger.DocumentNumber;
                existingPassenger.Email = passenger.Email;
                existingPassenger.ContactPhone = passenger.ContactPhone;
                existingPassenger.EmergencyContactName = passenger.EmergencyContactName;
                existingPassenger.EmergencyContactPhone = passenger.EmergencyContactPhone;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<bool> DeletePassenger(int id)
        {
            try
            {
                var passenger = await GetPassengerById(id);
                if (passenger != null)
                {
                    _context.Passengers.Remove(passenger);
                    await _context.SaveChangesAsync();
                    return true;
                }else return false;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<List<Passengers>> GetAllPassengers()
        {
            try
            {
                return await _context.Passengers.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
