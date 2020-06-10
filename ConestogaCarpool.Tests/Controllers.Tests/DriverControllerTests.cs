using ConestogaCarpool.BusinessLogic;
using ConestogaCarpool.Controllers;
using ConestogaCarpool.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ConestogaCarpool.Tests.Controllers.Tests
{
    public class DriverControllerTests
    {
        private readonly ConestogaCarpoolContext _context;

        [Fact]
        public void DriverControllerIndexReturnsView_ExpectedSuccess()
        {
            // Arrange
            var drivers = GetDrivers();
            var _mock = new Mock<IDriverLogic>();
            _mock.Setup(x => x.GetDrivers(It.IsAny<Int32>())).ReturnsAsync(drivers);
            var controllerUnderTest = new DriverController(_context, _mock.Object);

            // Act
            var result = controllerUnderTest.Index(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(Task<IActionResult>), result.GetType());
        }

        [Fact]
        public void DriverControllerCreateReturnsView_ExpectedSuccess()
        {
            // Arrange
            var drivers = GetDrivers();
            var _mock = new Mock<IDriverLogic>();
            _mock.Setup(x => x.GetDrivers(It.IsAny<Int32>())).ReturnsAsync(drivers);
            var controllerUnderTest = new DriverController(_context, _mock.Object);

            // Act
            var result = controllerUnderTest.Create(GetDriver());

            // Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void DriverControllerEditReturnsView_ExpectedSuccess()
        {
            // Arrange
            Driver driver = new Driver
            {
                DriverId = 2,
                UserId = 2,
                LicenceClassId = 2,
                Experience = 6
            };

            Mock<IDriverLogic> mockDriverLogic = new Mock<IDriverLogic>();
            mockDriverLogic.Setup(x => x.GetDriver(It.IsAny<Int32>())).ReturnsAsync(GetDriver());
            var controllerUnderTest = new DriverController(_context, mockDriverLogic.Object);

            // Act
            var result = controllerUnderTest.Edit(1, driver);

            // Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void DriverControllerDetailsReturnsView_ExpectedSuccess()
        {
            // Arrange
            Mock<IDriverLogic> mockDriverLogic = new Mock<IDriverLogic>();
            mockDriverLogic.Setup(x => x.GetDriver(It.IsAny<Int32>())).ReturnsAsync(GetDriver());
            var controllerUnderTest = new DriverController(_context, mockDriverLogic.Object);

            // Act
            var result = controllerUnderTest.Details(1);

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void DriverControllerDeleteConfirmedReturnsView_ExpectedSuccess()
        {
            // Arrange
            Mock<IDriverLogic> mockDriverLogic = new Mock<IDriverLogic>();
            mockDriverLogic.Setup(x => x.GetDriver(It.IsAny<Int32>())).ReturnsAsync(GetDriver());
            var controllerUnderTest = new DriverController(_context, mockDriverLogic.Object);

            // Act
            var result = controllerUnderTest.DeleteConfirmed(1);

            // Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        #region Private Methods
        private List<Driver> GetDrivers()
        {
            var drivers = new List<Driver>();
            drivers.Add(new Driver
            {
                DriverId = 1,
                UserId = 1,
                LicenceClassId = 1,
                Experience = 4
            });
            drivers.Add(new Driver
            {
                DriverId = 2,
                UserId = 2,
                LicenceClassId = 2,
                Experience = 5
            });

            return drivers;
        }

        private Driver GetDriver()
        {
            Driver driver = new Driver
            {
                DriverId = 2,
                UserId = 2,
                LicenceClassId = 2,
                Experience = 5
            };

            return driver;
        }
        #endregion

    }
}
