using AutoMapper;
using HotelAccommodationManagementApplication.Dto;
using HotelAccommodationManagementApplication.Services;
using HotelAccommodationManagementDomain.Entities;
using HotelAccommodationManagementDomain.Repositories;
using Moq;

namespace HotelAccommodationManagementTest.TestService
{
    public class PassengerServiceTest
    {
        private readonly Mock<IPassengerRepository> _passengerRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly PassengerService _passengerService;

        public PassengerServiceTest()
        {
            _passengerRepositoryMock = new Mock<IPassengerRepository>();
            _mapperMock = new Mock<IMapper>();
            _passengerService = new PassengerService(_passengerRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task AddPassenger_ShouldAddPassenger_WhenSuccessful()
        {
            // Arrange
            var passengerDto = new PassengerDto
            {
                Id = 1,
                ReservationId = 1,
                FirstName = "John",
                LastName = "Doe",
                BirthDate = new DateTime(1990, 1, 1),
                Gender = "Male",
                DocumentType = "Passport",
                DocumentNumber = "A1234567",
                Email = "john.doe@example.com",
                ContactPhone = "1234567890",
                EmergencyContactName = "Jane Doe",
                EmergencyContactPhone = "0987654321"
            };
            var passenger = new Passengers
            {
                Id = 1,
                ReservationId = 1,
                FirstName = "John",
                LastName = "Doe",
                BirthDate = new DateTime(1990, 1, 1),
                Gender = "Male",
                DocumentType = "Passport",
                DocumentNumber = "A1234567",
                Email = "john.doe@example.com",
                ContactPhone = "1234567890",
                EmergencyContactName = "Jane Doe",
                EmergencyContactPhone = "0987654321"
            };

            _mapperMock
                .Setup(m => m.Map<Passengers>(passengerDto))
                .Returns(passenger);

            _mapperMock
                .Setup(m => m.Map<PassengerDto>(passenger))
                .Returns(passengerDto);

            _passengerRepositoryMock
                .Setup(repo => repo.AddPassenger(passenger))
                .ReturnsAsync(passenger);

            // Act
            var result = await _passengerService.AddPassenger(passengerDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("200", result.Status);
            Assert.Equal("Petición exitosa", result.Message);
        }

        [Fact]
        public async Task DeletePassenger_ShouldDeletePassenger_WhenSuccessful()
        {
            // Arrange
            var id = 1;
            _passengerRepositoryMock
                .Setup(repo => repo.DeletePassenger(id))
                .ReturnsAsync(true);

            // Act
            var result = await _passengerService.DeletePassenger(id);

            // Assert
            Assert.Null(result.Data);
            Assert.Equal("200", result.Status);
            Assert.Equal("Petición exitosa", result.Message);
        }

        [Fact]
        public async Task GetAllPassengers_ShouldReturnPassengers_WhenSuccessful()
        {
            // Arrange
            var passengers = new List<Passengers>
            {
                new Passengers
                {
                    Id = 1,
                    ReservationId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    BirthDate = new DateTime(1990, 1, 1),
                    Gender = "Male",
                    DocumentType = "Passport",
                    DocumentNumber = "A1234567",
                    Email = "john.doe@example.com",
                    ContactPhone = "1234567890",
                    EmergencyContactName = "Jane Doe",
                    EmergencyContactPhone = "0987654321"
                },
                new Passengers
                {
                    Id = 2,
                    ReservationId = 2,
                    FirstName = "Alice",
                    LastName = "Smith",
                    BirthDate = new DateTime(1985, 5, 15),
                    Gender = "Female",
                    DocumentType = "ID Card",
                    DocumentNumber = "B7654321",
                    Email = "alice.smith@example.com",
                    ContactPhone = "2345678901",
                    EmergencyContactName = "Bob Smith",
                    EmergencyContactPhone = "1098765432"
                }
            };

            var passengerDtos = new List<PassengerDto>
            {
                new PassengerDto
                {
                    Id = 1,
                    ReservationId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    BirthDate = new DateTime(1990, 1, 1),
                    Gender = "Male",
                    DocumentType = "Passport",
                    DocumentNumber = "A1234567",
                    Email = "john.doe@example.com",
                    ContactPhone = "1234567890",
                    EmergencyContactName = "Jane Doe",
                    EmergencyContactPhone = "0987654321"
                },
                new PassengerDto
                {
                    Id = 2,
                    ReservationId = 2,
                    FirstName = "Alice",
                    LastName = "Smith",
                    BirthDate = new DateTime(1985, 5, 15),
                    Gender = "Female",
                    DocumentType = "ID Card",
                    DocumentNumber = "B7654321",
                    Email = "alice.smith@example.com",
                    ContactPhone = "2345678901",
                    EmergencyContactName = "Bob Smith",
                    EmergencyContactPhone = "1098765432"
                }
            };

            _passengerRepositoryMock
                .Setup(repo => repo.GetAllPassengers())
                .ReturnsAsync(passengers);

            _mapperMock
                .Setup(m => m.Map<List<PassengerDto>>(passengers))
                .Returns(passengerDtos);

            // Act
            var result = await _passengerService.GetAllPassengers();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("200", result.Status);
            Assert.Equal("Petición exitosa", result.Message);
            Assert.Equal(passengerDtos, result.Data);
        }

        [Fact]
        public async Task GetPassengerById_ShouldReturnPassenger_WhenSuccessful()
        {
            // Arrange
            var id = 1;
            var passenger = new Passengers
            {
                Id = id,
                ReservationId = 1,
                FirstName = "John",
                LastName = "Doe",
                BirthDate = new DateTime(1990, 1, 1),
                Gender = "Male",
                DocumentType = "Passport",
                DocumentNumber = "A1234567",
                Email = "john.doe@example.com",
                ContactPhone = "1234567890",
                EmergencyContactName = "Jane Doe",
                EmergencyContactPhone = "0987654321"
            };
            var passengerDto = new PassengerDto
            {
                Id = id,
                ReservationId = 1,
                FirstName = "John",
                LastName = "Doe",
                BirthDate = new DateTime(1990, 1, 1),
                Gender = "Male",
                DocumentType = "Passport",
                DocumentNumber = "A1234567",
                Email = "john.doe@example.com",
                ContactPhone = "1234567890",
                EmergencyContactName = "Jane Doe",
                EmergencyContactPhone = "0987654321"
            };

            _passengerRepositoryMock
                .Setup(repo => repo.GetPassengerById(id))
                .ReturnsAsync(passenger);

            _mapperMock
                .Setup(m => m.Map<PassengerDto>(passenger))
                .Returns(passengerDto);

            // Act
            var result = await _passengerService.GetPassengerById(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("200", result.Status);
            Assert.Equal("Petición exitosa", result.Message);
            Assert.Equal(passengerDto, result.Data);
        }

        [Fact]
        public async Task UpdatePassenger_ShouldUpdatePassenger_WhenSuccessful()
        {
            // Arrange
            var passengerDto = new PassengerDto
            {
                Id = 1,
                ReservationId = 1,
                FirstName = "John",
                LastName = "Doe",
                BirthDate = new DateTime(1990, 1, 1),
                Gender = "Male",
                DocumentType = "Passport",
                DocumentNumber = "A1234567",
                Email = "john.doe@example.com",
                ContactPhone = "1234567890",
                EmergencyContactName = "Jane Doe",
                EmergencyContactPhone = "0987654321"
            };
            var passenger = new Passengers
            {
                Id = 1,
                ReservationId = 1,
                FirstName = "John",
                LastName = "Doe",
                BirthDate = new DateTime(1990, 1, 1),
                Gender = "Male",
                DocumentType = "Passport",
                DocumentNumber = "A1234567",
                Email = "john.doe@example.com",
                ContactPhone = "1234567890",
                EmergencyContactName = "Jane Doe",
                EmergencyContactPhone = "0987654321"
            };

            _mapperMock
                .Setup(m => m.Map<Passengers>(passengerDto))
                .Returns(passenger);

            _passengerRepositoryMock
                .Setup(repo => repo.UpdatePassenger(passenger))
                .ReturnsAsync(true);

            // Act
            var result = await _passengerService.UpdatePassenger(passengerDto);

            // Assert
            Assert.Null(result.Data);
            Assert.Equal("200", result.Status);
            Assert.Equal("Petición exitosa", result.Message);
        }
    }
}
