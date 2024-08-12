using AutoMapper;
using HotelAccommodationManagementApplication.Dto;
using HotelAccommodationManagementDomain.Entities;
using HotelAccommodationManagementDomain.Repositories;

namespace HotelAccommodationManagementApplication.Services
{
    public class ReservationServices : ResponseBaseService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;
        private readonly ISendMail _sendMail;

        public ReservationServices(IReservationRepository reservationRepository, IMapper mapper, ISendMail sendMail)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
            _sendMail = sendMail;
        }

        public async Task<Response<ReservationDto>> AddReservation            
            (ReservationDto reservations) =>
            await HandleRequest<ReservationDto>(async () =>
            {
                var entity = _mapper.Map<Reservations>(reservations);
                entity.CreatedAt = DateTime.UtcNow;
                var response = _reservationRepository.AddReservation(entity);

                if (response.Id == 0)
                    throw new TaskCanceledException("No se pudo crear la reservacion");

                await _sendMail.SendReservationEmail(reservations.UserId, _mapper.Map<Reservations>(reservations));

                return _mapper.Map<ReservationDto>(response);
            });

        public async Task<Response<ReservationDto>> DeleteReservation(int id) =>
            await HandleRequest<ReservationDto>(async () =>
            {
                bool success = await _reservationRepository.DeleteReservation(id);

                if (!success)
                    throw new TaskCanceledException("No se pudo eliminar la reservacion");

                return null;
            });

        public async Task<Response<List<ReservationDto>>> GetAllReservations() =>
            await HandleRequest<List<ReservationDto>>(async () =>
            {
                var Reservations = await _reservationRepository.GetAllReservations();

                if (Reservations.Count == 0)
                    throw new TaskCanceledException("No se encontraron reservaciones");

                return _mapper.Map<List<ReservationDto>>(Reservations);
            });

        public async Task<Response<List<ReservationDto>>> GetReservationByUser(int idUser) =>
            await HandleRequest<List<ReservationDto>>(async () =>
            {
                var Reservations = await _reservationRepository.GetReservationByUser(idUser);

                if (Reservations.Count == 0)
                    throw new TaskCanceledException("No se encontraron reservaciones para ese usuario");

                return _mapper.Map<List<ReservationDto>>(Reservations);
            });

        public async Task<Response<ReservationDto>> GetReservationsById(int id) =>
            await HandleRequest<ReservationDto>(async () =>
            {
                var Reservations = await _reservationRepository.GetReservationsById(id);

                if (Reservations == null || Reservations.Id == 0)
                    throw new TaskCanceledException("No se encontró la reservacion");

                return _mapper.Map<ReservationDto>(Reservations);
            });

        public async Task<Response<ReservationDto>> UpdateReservation(ReservationDto reservation) =>
            await HandleRequest<ReservationDto>(async () =>
            {
                bool success = await _reservationRepository.UpdateReservation(_mapper.Map<Reservations>(reservation));

                if (!success)
                    throw new TaskCanceledException("No se pudo actualizar la reservacion");

                return null;
            });
    }
}
