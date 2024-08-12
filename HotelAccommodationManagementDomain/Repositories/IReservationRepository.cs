using HotelAccommodationManagementDomain.Entities;

namespace HotelAccommodationManagementDomain.Repositories
{
    public interface IReservationRepository
    {
        Task<Reservations> GetReservationsById(int id);
        Task<List<Reservations>> GetAllReservations();
        Task<List<Reservations>> GetReservationByUser(int idUser);
        Task<Reservations> AddReservation(Reservations reservations);
        Task<bool> UpdateReservation(Reservations reservations);
        Task<bool> DeleteReservation(int id);
    }
}
