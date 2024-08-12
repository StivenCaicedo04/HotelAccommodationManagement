using HotelAccommodationManagementApplication.Dto;
using HotelAccommodationManagementApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelAccommodationManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly HotelServices _hotelServices;

        public HotelsController(HotelServices hotelServices)
        {
            _hotelServices = hotelServices;
        }

        [HttpPost]
        public async Task<IActionResult> AddHotel([FromBody] HotelDto hotel)
        {
            var response = await _hotelServices.AddHotel(hotel);
            if (response.Status == "500")
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var response = await _hotelServices.DeleteHotel(id);
            if (response.Status == "500")
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            var response = await _hotelServices.GetAllHotels();
            if (response.Status == "500")
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var response = await _hotelServices.GetHotelById(id);
            if (response.Status == "500")
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHotel([FromBody] HotelDto hotel)
        {
            var response = await _hotelServices.UpdateHotel(hotel);
            if (response.Status == "500")
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }
    }
}
