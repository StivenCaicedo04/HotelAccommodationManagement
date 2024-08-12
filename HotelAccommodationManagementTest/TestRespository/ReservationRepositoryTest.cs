using HotelAccommodationManagementDomain.Entities;
using HotelAccommodationManagementDomain.Repositories.Repository;
using HotelAccommodationManagementInfrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelAccommodationManagementTest.TestRespository
{
    public class ReservationRepositoryTest
    {
        private readonly ReservationRepository _repository;
        private readonly DataDbContext _context;

        public ReservationRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<DataDbContext>()
                .UseInMemoryDatabase(databaseName: "ReservationTestDb")
                .Options;

            _context = new DataDbContext(options);
            _repository = new ReservationRepository(_context);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetReservationsById_ReturnsReservation_WhenReservationExists()
        {
            // Arrange
            var reservation = new Reservations
            {
                Id = 1,
                UserId = 1,
                RoomId = 1,
                CheckInDate = DateTime.UtcNow.AddDays(1),
                CheckOutDate = DateTime.UtcNow.AddDays(2),
                NumberOfGuests = 2
            };
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetReservationsById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.UserId);
        }

        [Fact]
        public async Task GetAllReservations_ReturnsListOfReservations()
        {
            // Arrange
            var reservations = new List<Reservations>
            {
                new Reservations
                {
                    Id = 2,
                    UserId = 1,
                    RoomId = 2,
                    CheckInDate = DateTime.UtcNow.AddDays(1),
                    CheckOutDate = DateTime.UtcNow.AddDays(3),
                    NumberOfGuests = 3
                },
                new Reservations
                {
                    Id = 3,
                    UserId = 2,
                    RoomId = 3,
                    CheckInDate = DateTime.UtcNow.AddDays(2),
                    CheckOutDate = DateTime.UtcNow.AddDays(4),
                    NumberOfGuests = 1
                }
            };
            _context.Reservations.AddRange(reservations);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllReservations();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, r => r.Id == 2);
            Assert.Contains(result, r => r.Id == 3);
        }

        [Fact]
        public async Task GetReservationByUser_ReturnsReservations_WhenReservationsExist()
        {
            // Arrange
            var reservations = new List<Reservations>
            {
                new Reservations
                {
                    Id = 4,
                    UserId = 3,
                    RoomId = 4,
                    CheckInDate = DateTime.UtcNow.AddDays(1),
                    CheckOutDate = DateTime.UtcNow.AddDays(2),
                    NumberOfGuests = 1
                },
                new Reservations
                {
                    Id = 5,
                    UserId = 3,
                    RoomId = 5,
                    CheckInDate = DateTime.UtcNow.AddDays(3),
                    CheckOutDate = DateTime.UtcNow.AddDays(4),
                    NumberOfGuests = 2
                }
            };
            _context.Reservations.AddRange(reservations);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetReservationByUser(3);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, r => r.Id == 4);
            Assert.Contains(result, r => r.Id == 5);
        }

        [Fact]
        public async Task AddReservation_AddsReservationSuccessfully()
        {
            // Arrange
            var reservation = new Reservations
            {
                Id = 6,
                UserId = 4,
                RoomId = 6,
                CheckInDate = DateTime.UtcNow.AddDays(1),
                CheckOutDate = DateTime.UtcNow.AddDays(2),
                NumberOfGuests = 2
            };

            // Act
            var result = await _repository.AddReservation(reservation);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(6, result.Id);
        }

        [Fact]
        public async Task UpdateReservation_UpdatesReservationSuccessfully()
        {
            // Arrange
            var existingReservation = new Reservations
            {
                Id = 7,
                UserId = 5,
                RoomId = 7,
                CheckInDate = DateTime.UtcNow.AddDays(1),
                CheckOutDate = DateTime.UtcNow.AddDays(3),
                NumberOfGuests = 1
            };
            _context.Reservations.Add(existingReservation);
            await _context.SaveChangesAsync();

            var updatedReservation = new Reservations
            {
                Id = 7,
                UserId = 6,
                RoomId = 8,
                CheckInDate = DateTime.UtcNow.AddDays(2),
                CheckOutDate = DateTime.UtcNow.AddDays(4),
                NumberOfGuests = 2
            };

            // Act
            var result = await _repository.UpdateReservation(updatedReservation);

            // Assert
            Assert.True(result);
            var updated = await _repository.GetReservationsById(7);
            Assert.Equal(6, updated.UserId);
            Assert.Equal(8, updated.RoomId);
        }

        [Fact]
        public async Task DeleteReservation_DeletesReservationSuccessfully()
        {
            // Arrange
            var reservation = new Reservations
            {
                Id = 8,
                UserId = 7,
                RoomId = 9,
                CheckInDate = DateTime.UtcNow.AddDays(1),
                CheckOutDate = DateTime.UtcNow.AddDays(2),
                NumberOfGuests = 2
            };
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.DeleteReservation(8);

            // Assert
            Assert.True(result);
            var deletedReservation = await _repository.GetReservationsById(8);
            Assert.Null(deletedReservation);
        }
    }
}
