using HotelAccommodationManagementDomain.Entities;
using HotelAccommodationManagementDomain.Repositories.Repository;
using HotelAccommodationManagementInfrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelAccommodationManagementTest.TestRespository
{
    public class RoomRepositoryTest
    {
        private readonly RoomRepository _repository;
        private readonly DataDbContext _context;

        public RoomRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<DataDbContext>()
                .UseInMemoryDatabase(databaseName: "RoomTestDb")
                .Options;

            _context = new DataDbContext(options);
            _repository = new RoomRepository(_context);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetRoomById_ReturnsRoom_WhenRoomExists()
        {
            // Arrange
            var room = new Rooms { Id = 1, HotelId = 1, RoomType = "Single", BaseCost = 100, Taxes = 10, Location = "1A", IsEnabled = true };
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetRoomById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Single", result.RoomType);
        }

        [Fact]
        public async Task GetRoomsByHotel_ReturnsRooms_WhenRoomsExist()
        {
            // Arrange
            var rooms = new List<Rooms>
            {
                new Rooms { Id = 2, HotelId = 1, RoomType = "Double", BaseCost = 200, Taxes = 20, Location = "2A", IsEnabled = true },
                new Rooms { Id = 3, HotelId = 1, RoomType = "Suite", BaseCost = 300, Taxes = 30, Location = "3A", IsEnabled = true }
            };
            _context.Rooms.AddRange(rooms);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetRoomsByHotel(1);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, r => r.RoomType == "Double");
            Assert.Contains(result, r => r.RoomType == "Suite");
        }

        [Fact]
        public async Task AddRoom_AddsRoomSuccessfully()
        {
            // Arrange
            var room = new Rooms { Id = 4, HotelId = 2, RoomType = "King", BaseCost = 400, Taxes = 40, Location = "4A", IsEnabled = true };

            // Act
            var result = await _repository.AddRoom(room);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("King", result.RoomType);
        }

        [Fact]
        public async Task UpdateRoom_UpdatesRoomSuccessfully()
        {
            // Arrange
            var existingRoom = new Rooms { Id = 5, HotelId = 3, RoomType = "Standard", BaseCost = 150, Taxes = 15, Location = "5A", IsEnabled = true };
            _context.Rooms.Add(existingRoom);
            await _context.SaveChangesAsync();

            var updatedRoom = new Rooms { Id = 5, HotelId = 3, RoomType = "Deluxe", BaseCost = 250, Taxes = 25, Location = "5B", IsEnabled = false };

            // Act
            var result = await _repository.UpdateRoom(updatedRoom);

            // Assert
            Assert.True(result);
            var updated = await _repository.GetRoomById(5);
            Assert.Equal("Deluxe", updated.RoomType);
        }

        [Fact]
        public async Task DeleteRoom_DeletesRoomSuccessfully()
        {
            // Arrange
            var room = new Rooms { Id = 6, HotelId = 4, RoomType = "Suite", BaseCost = 500, Taxes = 50, Location = "6A", IsEnabled = true };
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.DeleteRoom(6);

            // Assert
            Assert.True(result);
            var deletedRoom = await _repository.GetRoomById(6);
            Assert.Null(deletedRoom);
        }

        [Fact]
        public async Task GetAllRooms_ReturnsListOfRooms()
        {
            // Arrange
            var rooms = new List<Rooms>
            {
                new Rooms { Id = 7, HotelId = 5, RoomType = "Single", BaseCost = 120, Taxes = 12, Location = "7A", IsEnabled = true },
                new Rooms { Id = 8, HotelId = 5, RoomType = "Double", BaseCost = 220, Taxes = 22, Location = "8A", IsEnabled = true }
            };
            _context.Rooms.AddRange(rooms);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllRooms();

            // Assert
            Assert.Equal(2, result.Count);
        }
    }
}
