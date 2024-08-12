using HotelAccommodationManagementDomain.Entities;
using HotelAccommodationManagementDomain.Repositories.Repository;
using HotelAccommodationManagementInfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HotelAccommodationManagementTest.TestRespository
{
    public class HotelRepositoryTest
    {
        public class HotelRepositoryTests
        {
            private readonly HotelRepository _repository;
            private readonly DataDbContext _context;

            public HotelRepositoryTests()
            {
                var options = new DbContextOptionsBuilder<DataDbContext>()
                    .UseInMemoryDatabase(databaseName: "HotelTestDb")
                    .Options;

                _context = new DataDbContext(options);
                _repository = new HotelRepository(_context);

                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();
            }

            [Fact]
            public async Task GetHotelById_ReturnsHotel_WhenHotelExists()
            {
                // Arrange
                var hotel = new Hotels { Id = 1, Name = "Test Hotel", Address = "Test Address", IsEnabled = true };
                _context.Hotels.Add(hotel);
                await _context.SaveChangesAsync();

                // Act
                var result = await _repository.GetHotelById(1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Test Hotel", result.Name);
            }

            [Fact]
            public async Task AddHotel_AddsHotelSuccessfully()
            {
                // Arrange
                var hotel = new Hotels { Id = 2, Name = "New Hotel", Address = "New Address", IsEnabled = true };

                // Act
                var result = await _repository.AddHotel(hotel);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("New Hotel", result.Name);
                Assert.Equal(2, result.Id);
            }

            [Fact]
            public async Task UpdateHotel_UpdatesHotelSuccessfully()
            {
                // Arrange
                var hotel = new Hotels { Id = 3, Name = "Updated Hotel", Address = "Updated Address", IsEnabled = true };
                var existingHotel = new Hotels { Id = 3, Name = "Old Hotel", Address = "Old Address", IsEnabled = true };
                _context.Hotels.Add(existingHotel);
                await _context.SaveChangesAsync();

                // Act
                var result = await _repository.UpdateHotel(hotel);

                // Assert
                Assert.True(result);
                var updatedHotel = await _repository.GetHotelById(3);
                Assert.Equal("Updated Hotel", updatedHotel.Name);
            }

            [Fact]
            public async Task DeleteHotel_DeletesHotelSuccessfully()
            {
                // Arrange
                var hotel = new Hotels { Id = 4, Name = "Hotel to Delete", Address = "Delete Address", IsEnabled = true };
                _context.Hotels.Add(hotel);
                await _context.SaveChangesAsync();

                // Act
                var result = await _repository.DeleteHotel(4);

                // Assert
                Assert.True(result);
                var deletedHotel = await _repository.GetHotelById(4);
                Assert.Null(deletedHotel);
            }

            [Fact]
            public async Task GetAllHotels_ReturnsListOfHotels()
            {
                // Arrange
                var hotels = new List<Hotels>
            {
                new Hotels { Id = 5, Name = "Hotel 1", Address = "Address 1", IsEnabled = true },
                new Hotels { Id = 6, Name = "Hotel 2", Address = "Address 2", IsEnabled = true }
            };
                _context.Hotels.AddRange(hotels);
                await _context.SaveChangesAsync();

                // Act
                var result = await _repository.GetAllHotels();

                // Assert
                Assert.Equal(2, result.Count);
            }
        }
    }
}

