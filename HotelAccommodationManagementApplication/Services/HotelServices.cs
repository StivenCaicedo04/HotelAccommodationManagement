using AutoMapper;
using HotelAccommodationManagementApplication.Dto;
using HotelAccommodationManagementDomain.Entities;
using HotelAccommodationManagementDomain.Repositories;

namespace HotelAccommodationManagementApplication.Services
{
    public class HotelServices : ResponseBaseService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public HotelServices(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<Response<HotelDto>> AddHotel(HotelDto hotel) =>
            await HandleRequest<HotelDto>(async () =>
            {
                var entity = _mapper.Map<Hotels>(hotel);
                entity.CreatedAt = DateTime.UtcNow;
                entity.ModifiedAt = DateTime.UtcNow;
                var response = await _hotelRepository.AddHotel(entity);

                if (response.Id == 0)
                    throw new TaskCanceledException("No se pudo crear el hotel");

                return _mapper.Map<HotelDto>(response);
            });

        public async Task<Response<HotelDto>> DeleteHotel(int id) =>
            await HandleRequest<HotelDto>(async () =>
            {
                bool success = await _hotelRepository.DeleteHotel(id);

                if (!success)
                    throw new TaskCanceledException("No se pudo eliminar el hotel");

                return null;
            });

        public async Task<Response<List<HotelDto>>> GetAllHotels() =>
            await HandleRequest<List<HotelDto>>(async () =>
            {
                var hotels = await _hotelRepository.GetAllHotels();

                if (hotels.Count == 0)
                    throw new TaskCanceledException("No se encontraron hoteles");

                return _mapper.Map<List<HotelDto>>(hotels);
            });

        public async Task<Response<HotelDto>> GetHotelById(int id) =>
            await HandleRequest<HotelDto>(async () =>
            {
                var hotel = await _hotelRepository.GetHotelById(id);

                if (hotel == null || hotel.Id == 0)
                    throw new TaskCanceledException("No se encontró el hotel");

                return _mapper.Map<HotelDto>(hotel);
            });

        public async Task<Response<HotelDto>> UpdateHotel(HotelDto hotel) =>
            await HandleRequest<HotelDto>(async () =>
            {
                bool success = await _hotelRepository.UpdateHotel(_mapper.Map<Hotels>(hotel));

                if (!success)
                    throw new TaskCanceledException("No se pudo actualizar el hotel");

                return null;
            });
    }
}
