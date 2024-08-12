
namespace HotelAccommodationManagementDomain.Entities
{
    public class Reservations
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
