using HotelAccommodationManagementDomain.Entities;
using HotelAccommodationManagementInfrastructure.Data;
using HotelAccommodationManagementInfrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace HotelAccommodationManagementTest.TestRespository
{
    public class LoginRepositoryTest
    {
        private readonly LoginRepository _repository;
        private readonly DataDbContext _context;

        public LoginRepositoryTest()
        {
            // Configura la base de datos en memoria
            var options = new DbContextOptionsBuilder<DataDbContext>()
                .UseInMemoryDatabase(databaseName: "LoginTestDb")
                .Options;

            _context = new DataDbContext(options);
            _repository = new LoginRepository(_context);

            // Asegúrate de limpiar la base de datos antes de cada prueba
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task LoginUser_ReturnsTrue_WhenCredentialsAreCorrect()
        {
            // Arrange
            var user = new Users
            {
                Id = 1,
                UserName = "john_doe",
                Password = "password123",
                Email = "john.doe@example.com",
                Role = "Admin"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var login = new Login
            {
                Email = "john.doe@example.com",
                Password = "password123"
            };

            // Act
            var result = await _repository.LoginUser(login);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task LoginUser_ThrowsException_WhenCredentialsAreIncorrect()
        {
            // Arrange
            var user = new Users
            {
                Id = 2,
                UserName = "jane_smith",
                Password = "password456",
                Email = "jane.smith@example.com",
                Role = "User"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var login = new Login
            {
                Email = "jane.smith@example.com",
                Password = "wrongpassword"
            };

            // Act & Assert
            await Assert.ThrowsAsync<TaskCanceledException>(() => _repository.LoginUser(login));
        }

        [Fact]
        public async Task LoginUser_ThrowsException_WhenUserDoesNotExist()
        {
            // Arrange
            var login = new Login
            {
                Email = "nonexistent.user@example.com",
                Password = "somepassword"
            };

            // Act & Assert
            await Assert.ThrowsAsync<TaskCanceledException>(() => _repository.LoginUser(login));
        }
    }
}
