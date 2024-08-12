using AutoMapper;
using HotelAccommodationManagementApplication.Dto;
using HotelAccommodationManagementDomain.Entities;
using HotelAccommodationManagementDomain.Repositories;

namespace HotelAccommodationManagementApplication.Services
{
    public class UserService : ResponseBaseService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Response<UserDto>> AddUser
            (UserDto user) =>
            await HandleRequest<UserDto>(async () =>
            {
                var entity = _mapper.Map<Users>(user);
                entity.CreatedAt = DateTime.UtcNow;
                var response = _userRepository.AddUser(entity);

                if (response.Id == 0)
                    throw new TaskCanceledException("No se pudo crear al usuario");

                return _mapper.Map<UserDto>(response);
            });

        public async Task<Response<UserDto>> DeleteUser(int id) =>
           await HandleRequest<UserDto>(async () =>
           {
               bool success = await _userRepository.DeleteUser(id);

               if (!success)
                   throw new TaskCanceledException("No se pudo eliminar al usuario");

               return null;
           });

        public async Task<Response<List<UserDto>>> GetAllUsers() =>
            await HandleRequest<List<UserDto>>(async () =>
            {
                var users = await _userRepository.GetAllUsers();

                if (users.Count == 0)
                    throw new TaskCanceledException("No se encontraron usuarios");

                return _mapper.Map<List<UserDto>>(users);
            });

        public async Task<Response<UserDto>> GetUserById(int id) =>
           await HandleRequest<UserDto>(async () =>
           {
               var user = await _userRepository.GetUserById(id);

               if (user == null || user.Id == 0)
                   throw new TaskCanceledException("No se encontró el usuario");

               return _mapper.Map<UserDto>(user);
           });

        public async Task<Response<UserDto>> UpdateUser(UserDto user) =>
            await HandleRequest<UserDto>(async () =>
            {
                bool success = await _userRepository.UpdateUser(_mapper.Map<Users>(user));

                if (!success)
                    throw new TaskCanceledException("No se pudo actualizar al usuario");

                return null;
            });
    }
}
