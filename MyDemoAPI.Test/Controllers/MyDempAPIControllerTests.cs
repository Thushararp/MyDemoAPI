using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Service.Contracts;
using System.Collections.Generic;
using Xunit;

namespace MyDemoAPI.APIControllers.Controllers.Tests
{
    public class MyDemoAPIControllerTests
    {
        private readonly Mock<ILogger<MyDemoAPIController>> _loggerMock;
        private readonly Mock<IStarRezGameService> _starRezGameServiceMock;
        private readonly MyDemoAPIController _controller;

        public MyDemoAPIControllerTests()
        {
            _loggerMock = new Mock<ILogger<MyDemoAPIController>>();
            _starRezGameServiceMock = new Mock<IStarRezGameService>();
            _controller = new MyDemoAPIController(_loggerMock.Object, _starRezGameServiceMock.Object);
        }

        #region Unit Tests for 'GetAll'  Action Method
        [Theory]
        [InlineData(0, 1)]
        [InlineData(1001, 1)]
        [InlineData(1, 0)]
        [InlineData(1, 1001)]
        [InlineData(10, 5)]
        public void GetAll_InvalidRange_ReturnsBadRequest(int from, int to)
        {
            // Arrange

            // Act
            var result = _controller.GetAll(from, to);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.IsType<string>(badRequestResult.Value);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(500, 500)]
        [InlineData(1000, 1000)]
        public void GetAll_ValidRange_ReturnsOkResult(int from, int to)
        {
            // Arrange
            var expectedResult = new List<AllDto>();

            _starRezGameServiceMock.Setup(service => service.GetAll(from, to)).Returns(expectedResult);

            // Act
            var result = _controller.GetAll(from, to);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(expectedResult, okResult.Value);
        }
        #endregion

        #region Unit Tests for 'Validate'  Action Method
        [Fact]
        public void Validate_InvalidPayload_ReturnsBadRequest()
        {
            // Arrange
            var validatePayload = new ValidateDTO { KidIndex = -1, ExpectedReturn = "Star" };

            _controller.ModelState.AddModelError("KidIndex", "Value must be 1 and 1000");

            // Act
            var result = _controller.Validate(validatePayload);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public void Validate_ValidPayload_ReturnsOkResult()
        {
            // Arrange
            var validatePayload = new ValidateDTO { KidIndex = 3, ExpectedReturn = "Star" };
            var expectedResult = true;

            _starRezGameServiceMock.Setup(service => service.Validate(validatePayload)).Returns(expectedResult);

            // Act
            var result = _controller.Validate(validatePayload);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(expectedResult, okResult.Value);
        }
        #endregion
    }
}
