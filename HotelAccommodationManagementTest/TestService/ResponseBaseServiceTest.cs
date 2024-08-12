using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAccommodationManagementTest.TestService
{
    public class ResponseBaseServiceTest
    {
        private readonly TestResponseBaseService _service;

        public ResponseBaseServiceTest()
        {
            _service = new TestResponseBaseService();
        }

        [Fact]
        public async Task HandleRequest_ShouldReturnSuccessResponse_WhenActionSucceeds()
        {
            // Arrange
            var expectedData = "Test Data";
            Func<Task<string>> action = async () => expectedData;

            // Act
            var response = await _service.ExecuteRequest(action);

            // Assert
            Assert.Equal("200", response.Status);
            Assert.Equal("Petición exitosa", response.Message);
            Assert.Equal(expectedData, response.Data);
        }

        [Fact]
        public async Task HandleRequest_ShouldReturnErrorResponse_WhenActionFails()
        {
            // Arrange
            var expectedExceptionMessage = "Test Exception";
            Func<Task<string>> action = async () =>
            {
                throw new Exception(expectedExceptionMessage);
            };

            // Act
            var response = await _service.ExecuteRequest(action);

            // Assert
            Assert.Equal("500", response.Status);
            Assert.Equal(expectedExceptionMessage, response.Message);
            Assert.Null(response.Data);
        }
    }
}
