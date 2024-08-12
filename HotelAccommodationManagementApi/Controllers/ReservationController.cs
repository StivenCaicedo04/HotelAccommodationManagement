using HotelAccommodationManagementApplication.Dto;
using HotelAccommodationManagementApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelAccommodationManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationServices _reservationServices;

        public ReservationsController(ReservationServices reservationServices)
        {
            _reservationServices = reservationServices;
        }

        [HttpPost]
        public async Task<IActionResult> AddReservation([FromBody] ReservationDto reservation)
        {
            var response = await _reservationServices.AddReservation(reservation);
            if (response.Status == "500")
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var response = await _reservationServices.DeleteReservation(id);
            if (response.Status == "500")
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var response = await _reservationServices.GetAllReservations();
            if (response.Status == "500")
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationsById(int id)
        {
            var response = await _reservationServices.GetReservationsById(id);
            if (response.Status == "500")
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpGet("user/{idUser}")]
        public async Task<IActionResult> GetReservationByUser(int idUser)
        {
            var response = await _reservationServices.GetReservationByUser(idUser);
            if (response.Status == "500")
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReservation([FromBody] ReservationDto reservation)
        {
            var response = await _reservationServices.UpdateReservation(reservation);
            if (response.Status == "500")
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }
    }
}
