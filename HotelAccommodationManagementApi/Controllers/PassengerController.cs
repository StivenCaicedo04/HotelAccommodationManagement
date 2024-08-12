using HotelAccommodationManagementApplication.Dto;
using HotelAccommodationManagementApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelAccommodationManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PassengersController : ControllerBase
    {
        private readonly PassengerService _passengerService;

        public PassengersController(PassengerService passengerService)
        {
            _passengerService = passengerService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPassenger([FromBody] PassengerDto passenger)
        {
            var response = await _passengerService.AddPassenger(passenger);
            if (response.Status == "500")
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassenger(int id)
        {
            var response = await _passengerService.DeletePassenger(id);
            if (response.Status == "500")
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPassengers()
        {
            var response = await _passengerService.GetAllPassengers();
            if (response.Status == "500")
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPassengerById(int id)
        {
            var response = await _passengerService.GetPassengerById(id);
            if (response.Status == "500")
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePassenger([FromBody] PassengerDto passenger)
        {
            var response = await _passengerService.UpdatePassenger(passenger);
            if (response.Status == "500")
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }
    }
}
