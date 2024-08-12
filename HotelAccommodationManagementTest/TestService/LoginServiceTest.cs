using AutoMapper;
using HotelAccommodationManagementApplication.Services;
using HotelAccommodationManagementDomain.Entities;
using HotelAccommodationManagementDomain.Repositories;
using Moq;

namespace HotelAccommodationManagementTest.TestService
{
    public class LoginServiceTest
    {
        private readonly Mock<ILoginUserRepository> _loginUserRepositoryMock;
        private readonly LoginServices _loginServices;

        public LoginServiceTest()
        {
            _loginUserRepositoryMock = new Mock<ILoginUserRepository>();
            _loginServices = new LoginServices(_loginUserRepositoryMock.Object);
        }

        [Fact]
        public async Task LoginUser_ShouldReturnSuccess_WhenCredentialsAreValid()
        {
            // Arrange
            var login = new Login { Email = "test@example.com", Password = "password" };

            _loginUserRepositoryMock
                .Setup(repo => repo.LoginUser(It.IsAny<Login>()))
                .ReturnsAsync(true);

            // Act
            var response = await _loginServices.LoginUser(login);

            // Assert
            Assert.Equal("200", response.Status);
            Assert.Equal("Inicio de sesión exitoso", response.Message);
            Assert.True(response.Data);
        }

        [Fact]
        public async Task LoginUser_ShouldReturnFailure_WhenCredentialsAreInvalid()
        {
            // Arrange
            var login = new Login { Email = "test@example.com", Password = "wrongpassword" };

            _loginUserRepositoryMock
                .Setup(repo => repo.LoginUser(It.IsAny<Login>()))
                .ReturnsAsync(false);

            // Act
            var response = await _loginServices.LoginUser(login);

            // Assert
            Assert.Equal("500", response.Status);
            Assert.Equal("No se pudo iniciar sesión para el usuario", response.Message);
            Assert.False(response.Data);
        }
    }
}
