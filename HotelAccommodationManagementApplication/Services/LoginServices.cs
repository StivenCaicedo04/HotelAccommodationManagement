using HotelAccommodationManagementDomain.Entities;
using HotelAccommodationManagementDomain.Repositories;

namespace HotelAccommodationManagementApplication.Services
{
    public class LoginServices
    {
        private readonly ILoginUserRepository _loginUserRepository;

        public LoginServices(ILoginUserRepository loginUserRepository)
        {
            _loginUserRepository = loginUserRepository;
        }

        public async Task<Response<bool>> LoginUser(Login login) {

            bool success = await _loginUserRepository.LoginUser(login);

            if (!success)
            {
                return new Response<bool>
                {
                    Status = "500",
                    Message = "No se pudo iniciar sesión para el usuario",
                    Data = false
                };
            }

            return new Response<bool>
            {
                Status = "200",
                Message = "Inicio de sesión exitoso",
                Data = true
            };
        }           
    }
}
