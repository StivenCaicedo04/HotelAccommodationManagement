using AutoMapper;
using HotelAccommodationManagementApplication.Dto;
using HotelAccommodationManagementApplication.Services;
using HotelAccommodationManagementDomain.Entities;
using HotelAccommodationManagementDomain.Repositories;
using Moq;

namespace HotelAccommodationManagementTest.TestService
{
    public class HotelServicesTest
    {
        private readonly Mock<IHotelRepository> _hotelRepositoryMock;
        private readonly IMapper _mapper;
        private readonly HotelServices _hotelServices;

        public HotelServicesTest()
        {
            _hotelRepositoryMock = new Mock<IHotelRepository>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HotelDto, Hotels>();
                cfg.CreateMap<Hotels, HotelDto>();
            });
            _mapper = mapperConfig.CreateMapper();

            _hotelServices = new HotelServices(_hotelRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task AddHotel_ShouldAddHotelSuccessfully()
        {
            // Arrange
            var hotelDto = new HotelDto { Name = "Test Hotel", Address = "Test Address" };
            var hotelEntity = new Hotels { Id = 1, Name = "Test Hotel", Address = "Test Address" };

            _hotelRepositoryMock
                .Setup(repo => repo.AddHotel(It.IsAny<Hotels>()))
                .ReturnsAsync(hotelEntity);

            // Act
            var response = await _hotelServices.AddHotel(hotelDto);

            // Assert
            Assert.NotNull(response);
            Assert.Equal("Test Hotel", response.Data.Name);
            Assert.Equal("Test Address", response.Data.Address);
        }

        [Fact]
        public async Task DeleteHotel_ShouldDeleteHotelSuccessfully()
        {
            // Arrange
            var hotelId = 1;
            _hotelRepositoryMock
                .Setup(repo => repo.DeleteHotel(hotelId))
                .ReturnsAsync(true);

            // Act
            var response = await _hotelServices.DeleteHotel(hotelId);

            // Assert
            Assert.Null(response.Data); // Null porque no devuelve nada en caso de éxito
        }

        [Fact]
        public async Task GetAllHotels_ShouldReturnHotels()
        {
            // Arrange
            var hotels = new List<Hotels>
            {
                new Hotels { Id = 1, Name = "Test Hotel 1", Address = "Test Address 1" },
                new Hotels { Id = 2, Name = "Test Hotel 2", Address = "Test Address 2" }
            };
            _hotelRepositoryMock
                .Setup(repo => repo.GetAllHotels())
                .ReturnsAsync(hotels);

            // Act
            var response = await _hotelServices.GetAllHotels();

            // Assert
            Assert.NotNull(response);
            Assert.Equal(2, response.Data.Count);
        }

        [Fact]
        public async Task GetHotelById_ShouldReturnHotel()
        {
            // Arrange
            var hotelId = 1;
            var hotelEntity = new Hotels { Id = hotelId, Name = "Test Hotel", Address = "Test Address" };

            _hotelRepositoryMock
                .Setup(repo => repo.GetHotelById(hotelId))
                .ReturnsAsync(hotelEntity);

            // Act
            var response = await _hotelServices.GetHotelById(hotelId);

            // Assert
            Assert.NotNull(response);
            Assert.Equal("Test Hotel", response.Data.Name);
        }

        [Fact]
        public async Task UpdateHotel_ShouldUpdateHotelSuccessfully()
        {
            // Arrange
            var hotelDto = new HotelDto { Id = 1, Name = "Updated Hotel", Address = "Updated Address" };

            _hotelRepositoryMock
                .Setup(repo => repo.UpdateHotel(It.IsAny<Hotels>()))
                .ReturnsAsync(true);

            // Act
            var response = await _hotelServices.UpdateHotel(hotelDto);

            // Assert
            Assert.Null(response.Data); // Null porque no devuelve nada en caso de éxito
        }
    }
}
