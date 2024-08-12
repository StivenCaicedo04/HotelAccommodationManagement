using AutoMapper;
using HotelAccommodationManagementApplication.Dto;
using HotelAccommodationManagementDomain.Entities;
using HotelAccommodationManagementDomain.Repositories;

namespace HotelAccommodationManagementApplication.Services
{
    public class RoomServices : ResponseBaseService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public RoomServices(IRoomRepository roomServices, IMapper mapper)
        {
            _roomRepository = roomServices;
            _mapper = mapper;
        }

        public async Task<Response<RoomDto>> AddRoom
           (RoomDto room) =>
           await HandleRequest<RoomDto>(async () =>
           {
               var entity = _mapper.Map<Rooms>(room);
               entity.CreatedAt = DateTime.UtcNow;
               entity.ModifiedAt = DateTime.UtcNow;
               var response = _roomRepository.AddRoom(entity);

               if (response.Id == 0)
                   throw new TaskCanceledException("No se pudo crear la habitacion");

               return _mapper.Map<RoomDto>(response);
           });

        public async Task<Response<RoomDto>> DeleteRoom(int id) =>
            await HandleRequest<RoomDto>(async () =>
            {
                bool success = await _roomRepository.DeleteRoom(id);

                if (!success)
                    throw new TaskCanceledException("No se pudo eliminar habitacion");

                return null;
            });

        public async Task<Response<List<RoomDto>>> GetAllRooms() =>
            await HandleRequest<List<RoomDto>>(async () =>
            {
                var rooms = await _roomRepository.GetAllRooms();

                if (rooms.Count == 0)
                    throw new TaskCanceledException("No se encontraron habitaciones");

                return _mapper.Map<List<RoomDto>>(rooms);
            });

        public async Task<Response<RoomDto>> GetRoomById(int id) =>
            await HandleRequest<RoomDto>(async () =>
            {
                var room = await _roomRepository.GetRoomById(id);

                if (room == null || room.Id == 0)
                    throw new TaskCanceledException("No se encontró la habitacion");

                return _mapper.Map<RoomDto>(room);
            });

        public async Task<Response<List<RoomDto>>> GetRoomsByHotel(int idHotel) =>
            await HandleRequest<List<RoomDto>>(async () =>
            {
                var rooms = await _roomRepository.GetRoomsByHotel(idHotel);

                if (rooms.Count == 0)
                    throw new TaskCanceledException("No se encontraron habitaciones para ese hotel");

                return _mapper.Map<List<RoomDto>>(rooms);
            });

        public async Task<Response<RoomDto>> UpdateRoom(RoomDto room) =>
            await HandleRequest<RoomDto>(async () =>
            {
                bool success = await _roomRepository.UpdateRoom(_mapper.Map<Rooms>(room));

                if (!success)
                    throw new TaskCanceledException("No se pudo actualizar la habitacion");

                return null;
            });
    }
}
