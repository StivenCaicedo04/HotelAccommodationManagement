using HotelAccommodationManagementDomain.Entities;
using HotelAccommodationManagementInfrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelAccommodationManagementDomain.Repositories.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly DataDbContext _context;

        public ReservationRepository(DataDbContext context)
        {
            _context = context;
        }

        public async Task<Reservations> GetReservationsById(int id)
        {
            try
            {
                return await _context.Reservations.FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public async Task<List<Reservations>> GetAllReservations()
        {
            try
            {
                return await _context.Reservations.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<List<Reservations>> GetReservationByUser(int idUser)
        {
            try
            {
                return await _context.Reservations.Where(r => r.UserId == idUser).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<Reservations> AddReservation(Reservations reservation)
        {
            try
            {
                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();
                return _context.Reservations
                       .SingleOrDefault(h => h.Id == reservation.Id);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<bool> UpdateReservation(Reservations reservation)
        {
            try
            {
                var existingReservation = await _context.Reservations.FindAsync(reservation.Id);

                if (existingReservation == null)
                {
                    return false;
                }

                existingReservation.UserId = reservation.UserId;
                existingReservation.RoomId = reservation.RoomId;
                existingReservation.CheckInDate = reservation.CheckInDate;
                existingReservation.CheckOutDate = reservation.CheckOutDate;
                existingReservation.NumberOfGuests = reservation.NumberOfGuests;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<bool> DeleteReservation(int id)
        {
            try
            {
                var reservation = await GetReservationsById(id);
                if (reservation != null)
                {
                    _context.Reservations.Remove(reservation);
                    await _context.SaveChangesAsync();
                    return true;
                }else return false;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
