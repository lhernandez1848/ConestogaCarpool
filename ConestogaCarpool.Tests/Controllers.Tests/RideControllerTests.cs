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
    public class RideControllerTests
    {
        private readonly ConestogaCarpoolContext _context;

        [Fact]
        public void RideControllerDriverIndexReturnsView_ExpectedSuccess()
        {
            // Arrange
            var rides = GetRides();
            var _mock = new Mock<IRideLogic>();
            _mock.Setup(x => x.GetDriverRides(It.IsAny<Int32>())).ReturnsAsync(rides);
            var controllerUnderTest = new RideController(_context, _mock.Object);

            // Act
            var result = controllerUnderTest.DriverIndex(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(Task<IActionResult>), result.GetType());
        }

        [Fact]
        public void RideControllerPassengerIndexReturnsView_ExpectedSuccess()
        {
            // Arrange
            var rides = GetRides();
            var _mock = new Mock<IRideLogic>();
            _mock.Setup(x => x.GetPassengerRides(It.IsAny<Int32>())).ReturnsAsync(rides);
            var controllerUnderTest = new RideController(_context, _mock.Object);

            // Act
            var result = controllerUnderTest.PassengerIndex(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(Task<IActionResult>), result.GetType());
        }

        [Fact]
        public void RideControllerCreateReturnsView_ExpectedSuccess()
        {
            // Arrange
            var rides = GetRides();
            var _mock = new Mock<IRideLogic>();
            _mock.Setup(x => x.GetDriverRides(It.IsAny<Int32>())).ReturnsAsync(rides);
            var controllerUnderTest = new RideController(_context, _mock.Object);

            // Act
            var result = controllerUnderTest.Create(GetRide());

            // Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void RideControllerEditReturnsView_ExpectedSuccess()
        {
            // Arrange
            Ride ride = new Ride
            {
                RideId = 2,
                PostId = 2,
                RideStatusId = 2,
                RequestId = 2
            };

            var _mock = new Mock<IRideLogic>();
            _mock.Setup(x => x.GetSingleRide(It.IsAny<Int32>())).ReturnsAsync(GetRide());
            var controllerUnderTest = new RideController(_context, _mock.Object);

            // Act
            var result = controllerUnderTest.Edit(1, ride);

            // Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void RideControllerDetailsReturnsView_ExpectedSuccess()
        {
            // Arrange
            var _mock = new Mock<IRideLogic>();
            _mock.Setup(x => x.GetSingleRide(It.IsAny<Int32>())).ReturnsAsync(GetRide());
            var controllerUnderTest = new RideController(_context, _mock.Object);

            // Act
            var result = controllerUnderTest.Details(1);

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void RideControllerDeleteConfirmedReturnsView_ExpectedSuccess()
        {
            // Arrange
            var _mock = new Mock<IRideLogic>();
            _mock.Setup(x => x.GetSingleRide(It.IsAny<Int32>())).ReturnsAsync(GetRide());
            var controllerUnderTest = new RideController(_context, _mock.Object);

            // Act
            var result = controllerUnderTest.DeleteConfirmed(1);

            // Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        #region Private Methods

        private List<Ride> GetRides()
        {
            var rides = new List<Ride>();
            rides.Add(new Ride
            {
                RideId = 1,
                PostId = 1,
                RideStatusId = 1,
                RequestId = 1
            });
            rides.Add(new Ride
            {
                RideId = 2,
                PostId = 2,
                RideStatusId = 1,
                RequestId = 2
            });

            return rides;
        }

        private Ride GetRide()
        {
            Ride ride = new Ride
            {
                RideId = 2,
                PostId = 2,
                RideStatusId = 1,
                RequestId = 2
            };

            return ride;
        }
        #endregion
    }
}
