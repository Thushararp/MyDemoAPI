using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MyDemoAPI.APIControllers.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyDemoAPI.Test
{
    public class MyDempAPIControllerTest
    {

        [Fact]
        public void GetDemoAPIResult_ReturnsOkResult()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<MyDemoAPIController>>();
            var controller = new MyDemoAPIController(loggerMock.Object);

            // Act
            var result = controller.GetDemoAPIResult();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}

