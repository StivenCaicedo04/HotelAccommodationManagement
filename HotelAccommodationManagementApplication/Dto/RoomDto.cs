
namespace HotelAccommodationManagementApplication.Dto
{
    public class RoomDto
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string RoomType { get; set; }
        public decimal BaseCost { get; set; }
        public decimal Taxes { get; set; }
        public string Location { get; set; }
        public bool IsEnabled { get; set; }
    }
}
