using HotelAccommodationManagementApplication.Dto;
using HotelAccommodationManagementApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelAccommodationManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly RoomServices _roomServices;

        public RoomsController(RoomServices roomServices)
        {
            _roomServices = roomServices;
        }

        [HttpPost]
        public async Task<IActionResult> AddRoom([FromBody] RoomDto room)
        {
            var response = await _roomServices.AddRoom(room);
            if (response.Status == "500")
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var response = await _roomServices.DeleteRoom(id);
            if (response.Status == "500")
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            var response = await _roomServices.GetAllRooms();
            if (response.Status == "500")
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var response = await _roomServices.GetRoomById(id);
            if (response.Status == "500")
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpGet("hotel/{idHotel}")]
        public async Task<IActionResult> GetRoomsByHotel(int idHotel)
        {
            var response = await _roomServices.GetRoomsByHotel(idHotel);
            if (response.Status == "500")
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRoom([FromBody] RoomDto room)
        {
            var response = await _roomServices.UpdateRoom(room);
            if (response.Status == "500")
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }
    }
}
