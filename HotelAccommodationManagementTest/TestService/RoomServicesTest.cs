using AutoMapper;
using HotelAccommodationManagementApplication.Dto;
using HotelAccommodationManagementApplication.Services;
using HotelAccommodationManagementDomain.Entities;
using HotelAccommodationManagementDomain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAccommodationManagementTest.TestService
{
    public class RoomServicesTest
    {
        private readonly Mock<IRoomRepository> _mockRoomRepository;
        private readonly IMapper _mapper;
        private readonly RoomServices _roomServices;

        public RoomServicesTest()
        {
            _mockRoomRepository = new Mock<IRoomRepository>();

            // Configura AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RoomDto, Rooms>()
                    .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.RoomType));
                cfg.CreateMap<Rooms, RoomDto>()
                    .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.RoomType));
            });
            _mapper = config.CreateMapper();

            _roomServices = new RoomServices(_mockRoomRepository.Object, _mapper);
        }

        [Fact]
        public async Task AddRoom_ShouldReturnRoomDto_WhenRoomIsAdded()
        {
            // Arrange
            var roomDto = new RoomDto
            {
                Id = 1,
                HotelId = 1,
                RoomType = "Deluxe",
                BaseCost = 100,
                Taxes = 10,
                Location = "North Wing",
                IsEnabled = true
            };
            var roomEntity = _mapper.Map<Rooms>(roomDto);
            roomEntity.CreatedAt = DateTime.UtcNow;
            roomEntity.ModifiedAt = DateTime.UtcNow;

            _mockRoomRepository
                .Setup(r => r.AddRoom(It.IsAny<Rooms>()))
                .ReturnsAsync(roomEntity);

            // Act
            var result = await _roomServices.AddRoom(roomDto);

            // Assert
            Assert.Equal("200", result.Status);
            Assert.Equal("Petición exitosa", result.Message);
            Assert.NotNull(result.Data);
            Assert.Equal(roomDto.RoomType, result.Data.RoomType);
        }

        [Fact]
        public async Task DeleteRoom_ShouldReturnNull_WhenRoomIsDeleted()
        {
            // Arrange
            int roomId = 1;

            _mockRoomRepository
                .Setup(r => r.DeleteRoom(roomId))
                .ReturnsAsync(true);

            // Act
            var result = await _roomServices.DeleteRoom(roomId);

            // Assert
            Assert.Equal("200", result.Status);
            Assert.Equal("Petición exitosa", result.Message);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GetAllRooms_ShouldReturnListOfRoomDto_WhenRoomsExist()
        {
            // Arrange
            var rooms = new List<Rooms>
        {
            new Rooms { Id = 1, HotelId = 1, RoomType = "Deluxe", BaseCost = 100, Taxes = 10, Location = "North Wing", IsEnabled = true },
            new Rooms { Id = 2, HotelId = 1, RoomType = "Standard", BaseCost = 50, Taxes = 5, Location = "South Wing", IsEnabled = false }
        };

            _mockRoomRepository
                .Setup(r => r.GetAllRooms())
                .ReturnsAsync(rooms);

            // Act
            var result = await _roomServices.GetAllRooms();

            // Assert
            Assert.Equal("200", result.Status);
            Assert.Equal("Petición exitosa", result.Message);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count);
        }

        [Fact]
        public async Task GetRoomById_ShouldReturnRoomDto_WhenRoomExists()
        {
            // Arrange
            var roomId = 1;
            var roomEntity = new Rooms
            {
                Id = roomId,
                HotelId = 1,
                RoomType = "Deluxe",
                BaseCost = 100,
                Taxes = 10,
                Location = "North Wing",
                IsEnabled = true
            };

            _mockRoomRepository
                .Setup(r => r.GetRoomById(roomId))
                .ReturnsAsync(roomEntity);

            // Act
            var result = await _roomServices.GetRoomById(roomId);

            // Assert
            Assert.Equal("200", result.Status);
            Assert.Equal("Petición exitosa", result.Message);
            Assert.NotNull(result.Data);
            Assert.Equal(roomEntity.RoomType, result.Data.RoomType);
        }

        [Fact]
        public async Task GetRoomsByHotel_ShouldReturnListOfRoomDto_WhenRoomsExistForHotel()
        {
            // Arrange
            var hotelId = 1;
            var rooms = new List<Rooms>
        {
            new Rooms { Id = 1, HotelId = hotelId, RoomType = "Deluxe", BaseCost = 100, Taxes = 10, Location = "North Wing", IsEnabled = true },
            new Rooms { Id = 2, HotelId = hotelId, RoomType = "Standard", BaseCost = 50, Taxes = 5, Location = "South Wing", IsEnabled = false }
        };

            _mockRoomRepository
                .Setup(r => r.GetRoomsByHotel(hotelId))
                .ReturnsAsync(rooms);

            // Act
            var result = await _roomServices.GetRoomsByHotel(hotelId);

            // Assert
            Assert.Equal("200", result.Status);
            Assert.Equal("Petición exitosa", result.Message);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count);
        }

        [Fact]
        public async Task UpdateRoom_ShouldReturnNull_WhenRoomIsUpdated()
        {
            // Arrange
            var roomDto = new RoomDto
            {
                Id = 1,
                HotelId = 1,
                RoomType = "Suite",
                BaseCost = 150,
                Taxes = 15,
                Location = "East Wing",
                IsEnabled = true
            };
            var roomEntity = _mapper.Map<Rooms>(roomDto);

            _mockRoomRepository
                .Setup(r => r.UpdateRoom(It.IsAny<Rooms>()))
                .ReturnsAsync(true);

            // Act
            var result = await _roomServices.UpdateRoom(roomDto);

            // Assert
            Assert.Equal("200", result.Status);
            Assert.Equal("Petición exitosa", result.Message);
            Assert.Null(result.Data);
        }
    }
}
