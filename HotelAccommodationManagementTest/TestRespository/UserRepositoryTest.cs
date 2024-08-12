using HotelAccommodationManagementDomain.Entities;
using HotelAccommodationManagementDomain.Repositories.Repository;
using HotelAccommodationManagementInfrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelAccommodationManagementTest.TestRespository
{
    public class UserRepositoryTest
    {
        private readonly UserRepository _repository;
        private readonly DataDbContext _context;

        public UserRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<DataDbContext>()
                .UseInMemoryDatabase(databaseName: "UserTestDb")
                .Options;

            _context = new DataDbContext(options);
            _repository = new UserRepository(_context);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetUserById_ReturnsUser_WhenUserExists()
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

            // Act
            var result = await _repository.GetUserById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("john_doe", result.UserName);
            Assert.Equal("john.doe@example.com", result.Email);
        }

        [Fact]
        public async Task AddUser_AddsUserSuccessfully()
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

            // Act
            var result = await _repository.AddUser(user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
            Assert.Equal("jane_smith", result.UserName);
        }

        [Fact]
        public async Task UpdateUser_UpdatesUserSuccessfully()
        {
            // Arrange
            var existingUser = new Users
            {
                Id = 3,
                UserName = "bob_johnson",
                Password = "password789",
                Email = "bob.johnson@example.com",
                Role = "User"
            };
            _context.Users.Add(existingUser);
            await _context.SaveChangesAsync();

            var updatedUser = new Users
            {
                Id = 3,
                UserName = "robert_johnson",
                Password = "newpassword",
                Email = "robert.johnson@example.com",
                Role = "Admin"
            };

            // Act
            var result = await _repository.UpdateUser(updatedUser);

            // Assert
            Assert.True(result);
            var updated = await _repository.GetUserById(3);
            Assert.Equal("robert_johnson", updated.UserName);
            Assert.Equal("robert.johnson@example.com", updated.Email);
        }

        [Fact]
        public async Task DeleteUser_DeletesUserSuccessfully()
        {
            // Arrange
            var user = new Users
            {
                Id = 4,
                UserName = "charlie_brown",
                Password = "password101",
                Email = "charlie.brown@example.com",
                Role = "User"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.DeleteUser(4);

            // Assert
            Assert.True(result);
            var deletedUser = await _repository.GetUserById(4);
            Assert.Null(deletedUser);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsListOfUsers()
        {
            // Arrange
            var users = new List<Users>
            {
                new Users
                {
                    Id = 5,
                    UserName = "emily_davis",
                    Password = "password202",
                    Email = "emily.davis@example.com",
                    Role = "Admin"
                },
                new Users
                {
                    Id = 6,
                    UserName = "david_wilson",
                    Password = "password303",
                    Email = "david.wilson@example.com",
                    Role = "User"
                }
            };
            _context.Users.AddRange(users);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllUsers();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, u => u.Id == 5);
            Assert.Contains(result, u => u.Id == 6);
        }
    }
}

