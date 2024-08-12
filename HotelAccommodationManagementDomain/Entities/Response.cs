
namespace HotelAccommodationManagementDomain.Entities
{
    public class Response<T>
    {
        public string Status { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }        
    }
}
