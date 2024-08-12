using HotelAccommodationManagementApplication.Services;
using HotelAccommodationManagementDomain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelAccommodationManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginServices _loginServices;

        public LoginController(LoginServices loginServices)
        {
            _loginServices = loginServices;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        
        {
            if (login == null)
            {
                return BadRequest(new Response<bool>
                {
                    Status = "400",
                    Message = "Datos de login no proporcionados",
                    Data = false
                });
            }

            var response = await _loginServices.LoginUser(login);

            if (!response.Data)
            {
                return StatusCode(500, response);
            }

            return Ok(response);
        }
    }
}
