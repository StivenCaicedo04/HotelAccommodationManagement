using AutoMapper;
using HotelAccommodationManagementApplication.Dto;
using HotelAccommodationManagementDomain.Entities;
using HotelAccommodationManagementDomain.Repositories;

namespace HotelAccommodationManagementApplication.Services
{
    public class PassengerService : ResponseBaseService
    {
        private readonly IPassengerRepository _passengerRepository;
        private readonly IMapper _mapper;

        public PassengerService(IPassengerRepository passengerRepository, IMapper mapper)
        {
            _passengerRepository = passengerRepository;
            _mapper = mapper;
        }

        public async Task<Response<PassengerDto>> AddPassenger
            (PassengerDto passenger) =>
            await HandleRequest<PassengerDto>(async () =>
            {
                var entity = _mapper.Map<Passengers>(passenger);
                var response = _passengerRepository.AddPassenger(entity);

                if (response.Id == 0)
                    throw new TaskCanceledException("No se pudo crear al huesped");

                return _mapper.Map<PassengerDto>(response);
            });

        public async Task<Response<PassengerDto>> DeletePassenger(int id) =>
           await HandleRequest<PassengerDto>(async () =>
           {
               bool success = await _passengerRepository.DeletePassenger(id);

               if (!success)
                   throw new TaskCanceledException("No se pudo eliminar al huesped");

               return null;
           });

        public async Task<Response<List<PassengerDto>>> GetAllPassengers() =>
            await HandleRequest<List<PassengerDto>>(async () =>
            {
                var passenger = await _passengerRepository.GetAllPassengers();

                if (passenger.Count == 0)
                    throw new TaskCanceledException("No se encontraron huespedes");

                return _mapper.Map<List<PassengerDto>>(passenger);
            });

        public async Task<Response<PassengerDto>> GetPassengerById(int id) =>
          await HandleRequest<PassengerDto>(async () =>
          {
              var passenger = await _passengerRepository.GetPassengerById(id);

              if (passenger == null || passenger.Id == 0)
                  throw new TaskCanceledException("No se encontró el huesped");

              return _mapper.Map<PassengerDto>(passenger);
          });

        public async Task<Response<PassengerDto>> UpdatePassenger(PassengerDto passenger) =>
             await HandleRequest<PassengerDto>(async () =>
             {
                 bool success = await _passengerRepository.UpdatePassenger(_mapper.Map<Passengers>(passenger));

                 if (!success)
                     throw new TaskCanceledException("No se pudo actualizar al huesped");

                 return null;
             });
    }
}
