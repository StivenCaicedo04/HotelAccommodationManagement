using HotelAccommodationManagementDomain.Entities;
using HotelAccommodationManagementDomain.Repositories.Repository;
using HotelAccommodationManagementInfrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelAccommodationManagementTest.TestRespository
{
    public class PassengerRepositoryTest
    {
        private readonly PassengerRepository _repository;
        private readonly DataDbContext _context;

        public PassengerRepositoryTest()
        {
            // Configura la base de datos en memoria
            var options = new DbContextOptionsBuilder<DataDbContext>()
                .UseInMemoryDatabase(databaseName: "PassengerTestDb")
                .Options;

            _context = new DataDbContext(options);
            _repository = new PassengerRepository(_context);

            // Asegúrate de limpiar la base de datos antes de cada prueba
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetPassengerById_ReturnsPassenger_WhenPassengerExists()
        {
            // Arrange
            var passenger = new Passengers
            {
                Id = 1,
                ReservationId = 1,
                FirstName = "John",
                LastName = "Doe",
                BirthDate = new DateTime(1990, 1, 1),
                Gender = "Male",
                DocumentType = "ID",
                DocumentNumber = "123456789",
                Email = "john.doe@example.com",
                ContactPhone = "1234567890",
                EmergencyContactName = "Jane Doe",
                EmergencyContactPhone = "0987654321"
            };
            _context.Passengers.Add(passenger);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetPassengerById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
        }

        [Fact]
        public async Task AddPassenger_AddsPassengerSuccessfully()
        {
            // Arrange
            var passenger = new Passengers
            {
                Id = 2,
                ReservationId = 2,
                FirstName = "Alice",
                LastName = "Smith",
                BirthDate = new DateTime(1992, 2, 2),
                Gender = "Female",
                DocumentType = "Passport",
                DocumentNumber = "987654321",
                Email = "alice.smith@example.com",
                ContactPhone = "1122334455",
                EmergencyContactName = "Bob Smith",
                EmergencyContactPhone = "5566778899"
            };

            // Act
            var result = await _repository.AddPassenger(passenger);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
            Assert.Equal("Alice", result.FirstName);
        }

        [Fact]
        public async Task UpdatePassenger_UpdatesPassengerSuccessfully()
        {
            // Arrange
            var existingPassenger = new Passengers
            {
                Id = 3,
                ReservationId = 3,
                FirstName = "Bob",
                LastName = "Brown",
                BirthDate = new DateTime(1985, 3, 3),
                Gender = "Male",
                DocumentType = "ID",
                DocumentNumber = "567890123",
                Email = "bob.brown@example.com",
                ContactPhone = "2233445566",
                EmergencyContactName = "Alice Brown",
                EmergencyContactPhone = "6677889900"
            };
            _context.Passengers.Add(existingPassenger);
            await _context.SaveChangesAsync();

            var updatedPassenger = new Passengers
            {
                Id = 3,
                ReservationId = 4,
                FirstName = "Robert",
                LastName = "Brown",
                BirthDate = new DateTime(1985, 3, 3),
                Gender = "Male",
                DocumentType = "Passport",
                DocumentNumber = "567890123",
                Email = "robert.brown@example.com",
                ContactPhone = "3344556677",
                EmergencyContactName = "Alice Brown",
                EmergencyContactPhone = "9988776655"
            };

            // Act
            var result = await _repository.UpdatePassenger(updatedPassenger);

            // Assert
            Assert.True(result);
            var updated = await _repository.GetPassengerById(3);
            Assert.Equal("Robert", updated.FirstName);
            Assert.Equal("Brown", updated.LastName);
            Assert.Equal("robert.brown@example.com", updated.Email);
        }

        [Fact]
        public async Task DeletePassenger_DeletesPassengerSuccessfully()
        {
            // Arrange
            var passenger = new Passengers
            {
                Id = 4,
                ReservationId = 5,
                FirstName = "Charlie",
                LastName = "Johnson",
                BirthDate = new DateTime(1975, 4, 4),
                Gender = "Male",
                DocumentType = "ID",
                DocumentNumber = "345678901",
                Email = "charlie.johnson@example.com",
                ContactPhone = "4455667788",
                EmergencyContactName = "Debbie Johnson",
                EmergencyContactPhone = "2233445566"
            };
            _context.Passengers.Add(passenger);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.DeletePassenger(4);

            // Assert
            Assert.True(result);
            var deletedPassenger = await _repository.GetPassengerById(4);
            Assert.Null(deletedPassenger);
        }

        [Fact]
        public async Task GetAllPassengers_ReturnsListOfPassengers()
        {
            // Arrange
            var passengers = new List<Passengers>
            {
                new Passengers
                {
                    Id = 5,
                    ReservationId = 6,
                    FirstName = "Emily",
                    LastName = "Davis",
                    BirthDate = new DateTime(1988, 5, 5),
                    Gender = "Female",
                    DocumentType = "ID",
                    DocumentNumber = "456789012",
                    Email = "emily.davis@example.com",
                    ContactPhone = "5566778899",
                    EmergencyContactName = "Michael Davis",
                    EmergencyContactPhone = "1122334455"
                },
                new Passengers
                {
                    Id = 6,
                    ReservationId = 7,
                    FirstName = "David",
                    LastName = "Wilson",
                    BirthDate = new DateTime(1982, 6, 6),
                    Gender = "Male",
                    DocumentType = "Passport",
                    DocumentNumber = "678901234",
                    Email = "david.wilson@example.com",
                    ContactPhone = "6677889900",
                    EmergencyContactName = "Laura Wilson",
                    EmergencyContactPhone = "2233445566"
                }
            };
            _context.Passengers.AddRange(passengers);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllPassengers();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p.Id == 5);
            Assert.Contains(result, p => p.Id == 6);
        }
    }
}
