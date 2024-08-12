using AutoMapper;
using HotelAccommodationManagementApplication.Dto;
using HotelAccommodationManagementDomain.Entities;

namespace HotelAccommodationManagementInfrastructure.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<Hotels, HotelDto>().ReverseMap()
            .ForMember(src => src.CreatedAt, opt => opt.Ignore())
            .ForMember(src => src.ModifiedAt, opt => opt.Ignore());
            CreateMap<Rooms, RoomDto>().ReverseMap();
            CreateMap<Reservations, ReservationDto>().ReverseMap();
            CreateMap<Passengers, PassengerDto>().ReverseMap();
            CreateMap<Users, UserDto>().ReverseMap();
        }
    }
}
