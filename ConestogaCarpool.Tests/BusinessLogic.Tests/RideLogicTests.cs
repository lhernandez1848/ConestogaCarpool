using ConestogaCarpool.BusinessLogic;
using ConestogaCarpool.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConestogaCarpool.Tests.BusinessLogic.Tests
{
    public class RideLogicTests
    {
        [Fact]
        public void GetAllRidesViaBLL_ExpectedSuccess()
        {
            // Arrange
            Mock<IRideLogic> mockRideLogic = new Mock<IRideLogic>();
            var expectedResult = new List<Ride>();
            expectedResult.Add(new Ride
            {
                RideId = 1,
                PostId = 1,
                RideStatusId = 1,
                RequestId = 1
            });
            expectedResult.Add(new Ride
            {
                RideId = 2,
                PostId = 2,
                RideStatusId = 1,
                RequestId = 2
            });
            var methodUnderTest = mockRideLogic.Object;

            // Set-up mock logic
            mockRideLogic.Setup(m => m.GetAllRides())
                .ReturnsAsync(expectedResult);

            // Act
            var allRides = methodUnderTest.GetAllRides();
            bool ridesReturned = true;

            if (!allRides.IsCompleted)
            {
                ridesReturned = false;
            }

            // Assert
            Assert.True(ridesReturned);
        }

        [Fact]
        public void GetDriverRidesViaBLL_ValidDriverId_ExpectedSuccess()
        {
            // Arrange
            Mock<IRideLogic> mockRideLogic = new Mock<IRideLogic>();
            var expectedResult = new List<Ride>();
            expectedResult.Add(new Ride
            {
                RideId = 1,
                PostId = 1,
                RideStatusId = 1,
                RequestId = 1
            });
            expectedResult.Add(new Ride
            {
                RideId = 2,
                PostId = 2,
                RideStatusId = 1,
                RequestId = 2
            });
            var methodUnderTest = mockRideLogic.Object;

            // Set-up mock logic
            mockRideLogic.Setup(m => m.GetDriverRides(It.IsAny<Int32>()))
                .ReturnsAsync(expectedResult);

            // Act
            var allRides = methodUnderTest.GetDriverRides(1);
            bool reviewsReturned = true;

            if (!allRides.IsCompleted)
            {
                reviewsReturned = false;
            }

            // Assert
            Assert.True(reviewsReturned);
        }

        [Fact]
        public void GetPassengerRidesViaBLL_ValidPassengerId_ExpectedSuccess()
        {
            // Arrange
            Mock<IRideLogic> mockRideLogic = new Mock<IRideLogic>();
            var expectedResult = new List<Ride>();
            expectedResult.Add(new Ride
            {
                RideId = 1,
                PostId = 1,
                RideStatusId = 1,
                RequestId = 1
            });
            expectedResult.Add(new Ride
            {
                RideId = 2,
                PostId = 2,
                RideStatusId = 1,
                RequestId = 2
            });
            var methodUnderTest = mockRideLogic.Object;

            // Set-up mock logic
            mockRideLogic.Setup(m => m.GetPassengerRides(It.IsAny<Int32>()))
                .ReturnsAsync(expectedResult);

            // Act
            var allPassengerRides = methodUnderTest.GetPassengerRides(1);
            bool ridesReturned = true;

            if (!allPassengerRides.IsCompleted)
            {
                ridesReturned = false;
            }

            // Assert
            Assert.True(ridesReturned);
        }

        [Fact]
        public void GetSingleRideViaBLL_ValidRideId_ReturnsRideDetails()
        {
            // Arrange
            Mock<IRideLogic> mockRideLogic = new Mock<IRideLogic>();
            var expectedResult = new Ride();
            var methodUnderTest = mockRideLogic.Object;

            // Set-up mock logic
            mockRideLogic.Setup(m => m.GetSingleRide(It.IsAny<Int32>()))
                .ReturnsAsync(expectedResult);

            // Act
            var rideFound = methodUnderTest.GetSingleRide(1);
            bool isRideFound = true;

            if (rideFound == null)
            {
                isRideFound = false;
            }

            // Assert
            Assert.True(isRideFound);
        }

        [Fact]
        public void CreateRideViaBLL_ValidRide_CreateMethodCalledOnce()
        {
            // Arrange
            Mock<IRideLogic> mockRideLogic = new Mock<IRideLogic>();
            var methodUnderTest = mockRideLogic.Object;
            var newRide = new Ride
            {
                RideId = 3,
                PostId = 7,
                RideStatusId = 1,
                RequestId = 5
            };

            // Set-up mock logic
            mockRideLogic.Setup(m => m.CreateRide(newRide));
            mockRideLogic.Setup(m => m.Save());

            // Act
            methodUnderTest.CreateRide(newRide);
            methodUnderTest.Save();

            // Assert
            mockRideLogic.Verify(x => x.CreateRide(newRide), Times.Once());
        }

        [Fact]
        public void UpdateRideViaBLL_ChangeRideStatus_UpdateMethodCalledOnce()
        {
            // Arrange
            Mock<IRideLogic> mockRideLogic = new Mock<IRideLogic>();
            var methodUnderTest = mockRideLogic.Object;
            var updatedRide = new Ride
            {
                RideId = 3,
                PostId = 7,
                RideStatusId = 2,
                RequestId = 5
            };

            // Set-up mock logic
            mockRideLogic.Setup(m => m.UpdateRide(updatedRide));
            mockRideLogic.Setup(m => m.Save());

            // Act
            methodUnderTest.UpdateRide(updatedRide);
            methodUnderTest.Save();

            // Assert
            mockRideLogic.Verify(x => x.UpdateRide(updatedRide), Times.Once());
        }

        [Fact]
        public void DeleteRideViaBLL_ValidRideId_DeleteMethodCalledOnce()
        {
            // Arrange
            Mock<IRideLogic> mockRideLogic = new Mock<IRideLogic>();
            var methodUnderTest = mockRideLogic.Object;
            var ride = new Ride
            {
                RideId = 3,
                PostId = 7,
                RideStatusId = 2,
                RequestId = 5
            };

            // Set-up mock logic
            mockRideLogic.Setup(m => m.DeleteRide(ride.RideId));
            mockRideLogic.Setup(m => m.Save());

            // Act
            methodUnderTest.DeleteRide(ride.RideId);
            methodUnderTest.Save();

            // Assert
            mockRideLogic.Verify(x => x.DeleteRide(ride.RideId), Times.Once());
        }
    }
}
