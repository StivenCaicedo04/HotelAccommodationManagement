using HotelAccommodationManagementApplication.Dto;
using HotelAccommodationManagementApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelAccommodationManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserDto user)
        {
            var response = await _userService.AddUser(user);
            if (response.Status == "500")
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);
            if (response.Status == "500")
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userService.GetAllUsers();
            if (response.Status == "500")
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var response = await _userService.GetUserById(id);
            if (response.Status == "500")
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto user)
        {
            var response = await _userService.UpdateUser(user);
            if (response.Status == "500")
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }
    }
}
