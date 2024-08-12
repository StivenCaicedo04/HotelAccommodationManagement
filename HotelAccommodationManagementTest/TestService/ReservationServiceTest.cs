using AutoMapper;
using HotelAccommodationManagementApplication.Dto;
using HotelAccommodationManagementApplication.Services;
using HotelAccommodationManagementDomain.Entities;
using HotelAccommodationManagementDomain.Repositories;
using Moq;

namespace HotelAccommodationManagementTest.TestService
{
    public class ReservationServiceTest
    {
        private readonly Mock<IReservationRepository> _reservationRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ISendMail> _sendMailMock;
        private readonly ReservationServices _reservationServices;

        public ReservationServiceTest()
        {
            _reservationRepositoryMock = new Mock<IReservationRepository>();
            _mapperMock = new Mock<IMapper>();
            _sendMailMock = new Mock<ISendMail>();
            _reservationServices = new ReservationServices(
                _reservationRepositoryMock.Object,
                _mapperMock.Object,
                _sendMailMock.Object
            );
        }

        [Fact]
        public async Task AddReservation_ShouldAddReservation_WhenSuccessful()
        {
            // Arrange
            var reservationDto = new ReservationDto
            {
                Id = 1,
                UserId = 1,
                RoomId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(1),
                NumberOfGuests = 2
            };
            var reservation = new Reservations
            {
                Id = 1,
                UserId = 1,
                RoomId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(1),
                NumberOfGuests = 2,
                CreatedAt = DateTime.Now
            };

            _mapperMock
                .Setup(m => m.Map<Reservations>(reservationDto))
                .Returns(reservation);
            _mapperMock
                .Setup(m => m.Map<ReservationDto>(reservation))
                .Returns(reservationDto);
            _reservationRepositoryMock
                .Setup(repo => repo.AddReservation(It.IsAny<Reservations>()))
                .ReturnsAsync(reservation);
            _sendMailMock
                .Setup(mail => mail.SendReservationEmail(It.IsAny<int>(), It.IsAny<Reservations>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _reservationServices.AddReservation(reservationDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("200", result.Status);
            Assert.Equal("Petición exitosa", result.Message);
        }

        [Fact]
        public async Task DeleteReservation_ShouldReturnNull_WhenSuccessful()
        {
            // Arrange
            int reservationId = 1;
            _reservationRepositoryMock
                .Setup(repo => repo.DeleteReservation(reservationId))
                .ReturnsAsync(true);

            // Act
            var result = await _reservationServices.DeleteReservation(reservationId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("200", result.Status);
            Assert.Equal("Petición exitosa", result.Message);
        }

        [Fact]
        public async Task GetAllReservations_ShouldReturnListOfReservations_WhenSuccessful()
        {
            // Arrange
            var reservations = new List<Reservations>
            {
                new Reservations
                {
                    Id = 1,
                    UserId = 1,
                    RoomId = 1,
                    CheckInDate = DateTime.Now,
                    CheckOutDate = DateTime.Now.AddDays(1),
                    NumberOfGuests = 2,
                    CreatedAt = DateTime.Now
                }
            };
            var reservationDtos = new List<ReservationDto>
            {
                new ReservationDto
                {
                    Id = 1,
                    UserId = 1,
                    RoomId = 1,
                    CheckInDate = DateTime.Now,
                    CheckOutDate = DateTime.Now.AddDays(1),
                    NumberOfGuests = 2
                }
            };

            _reservationRepositoryMock
                .Setup(repo => repo.GetAllReservations())
                .ReturnsAsync(reservations);
            _mapperMock
                .Setup(m => m.Map<List<ReservationDto>>(reservations))
                .Returns(reservationDtos);

            // Act
            var result = await _reservationServices.GetAllReservations();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("200", result.Status);
            Assert.Equal("Petición exitosa", result.Message);
            Assert.Equal(reservationDtos, result.Data);
        }

        [Fact]
        public async Task GetReservationByUser_ShouldReturnListOfReservations_WhenSuccessful()
        {
            // Arrange
            var userId = 1;
            var reservations = new List<Reservations>
            {
                new Reservations
                {
                    Id = 1,
                    UserId = 1,
                    RoomId = 1,
                    CheckInDate = DateTime.Now,
                    CheckOutDate = DateTime.Now.AddDays(1),
                    NumberOfGuests = 2,
                    CreatedAt = DateTime.Now
                }
            };
            var reservationDtos = new List<ReservationDto>
            {
                new ReservationDto
                {
                    Id = 1,
                    UserId = 1,
                    RoomId = 1,
                    CheckInDate = DateTime.Now,
                    CheckOutDate = DateTime.Now.AddDays(1),
                    NumberOfGuests = 2
                }
            };

            _reservationRepositoryMock
                .Setup(repo => repo.GetReservationByUser(userId))
                .ReturnsAsync(reservations);
            _mapperMock
                .Setup(m => m.Map<List<ReservationDto>>(reservations))
                .Returns(reservationDtos);

            // Act
            var result = await _reservationServices.GetReservationByUser(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("200", result.Status);
            Assert.Equal("Petición exitosa", result.Message);
            Assert.Equal(reservationDtos, result.Data);
        }

        [Fact]
        public async Task GetReservationsById_ShouldReturnReservation_WhenSuccessful()
        {
            // Arrange
            var reservationId = 1;
            var reservation = new Reservations
            {
                Id = 1,
                UserId = 1,
                RoomId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(1),
                NumberOfGuests = 2,
                CreatedAt = DateTime.Now
            };
            var reservationDto = new ReservationDto
            {
                Id = 1,
                UserId = 1,
                RoomId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(1),
                NumberOfGuests = 2
            };

            _reservationRepositoryMock
                .Setup(repo => repo.GetReservationsById(reservationId))
                .ReturnsAsync(reservation);
            _mapperMock
                .Setup(m => m.Map<ReservationDto>(reservation))
                .Returns(reservationDto);

            // Act
            var result = await _reservationServices.GetReservationsById(reservationId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("200", result.Status);
            Assert.Equal("Petición exitosa", result.Message);
            Assert.Equal(reservationDto, result.Data);
        }

        [Fact]
        public async Task UpdateReservation_ShouldReturnNull_WhenSuccessful()
        {
            // Arrange
            var reservationDto = new ReservationDto
            {
                Id = 1,
                UserId = 1,
                RoomId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(1),
                NumberOfGuests = 2
            };
            var reservation = new Reservations
            {
                Id = 1,
                UserId = 1,
                RoomId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(1),
                NumberOfGuests = 2,
                CreatedAt = DateTime.Now
            };

            _mapperMock
                .Setup(m => m.Map<Reservations>(reservationDto))
                .Returns(reservation);
            _reservationRepositoryMock
                .Setup(repo => repo.UpdateReservation(reservation))
                .ReturnsAsync(true);

            // Act
            var result = await _reservationServices.UpdateReservation(reservationDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("200", result.Status);
            Assert.Equal("Petición exitosa", result.Message);
        }
    }
}
