using ConestogaCarpool.Models;
using ConestogaCarpool.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConestogaCarpool.Tests.Repositories.Tests
{
    public class RideRepositoryTests
    {
        [Fact]
        public void GetAllRidesViaRepository_ExpectedSuccess()
        {
            // Arrange
            Mock<IRideRepository> mockRideRepository = new Mock<IRideRepository>();
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
            var methodUnderTest = mockRideRepository.Object;

            // Set-up mock repository
            mockRideRepository.Setup(m => m.GetAllRides())
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
        public void GetDriverRidesViaRepository_ValidDriverId_ExpectedSuccess()
        {
            // Arrange
            Mock<IRideRepository> mockRideRepository = new Mock<IRideRepository>();
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
            var methodUnderTest = mockRideRepository.Object;

            // Set-up mock repository
            mockRideRepository.Setup(m => m.GetDriverRides(It.IsAny<Int32>()))
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
        public void GetPassengerRidesViaRepository_ValidPassengerId_ExpectedSuccess()
        {
            // Arrange
            Mock<IRideRepository> mockRideRepository = new Mock<IRideRepository>();
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
            var methodUnderTest = mockRideRepository.Object;

            // Set-up mock repository
            mockRideRepository.Setup(m => m.GetPassengerRides(It.IsAny<Int32>()))
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
        public void GetSingleRideViaRepository_ValidRideId_ReturnsRideDetails()
        {
            // Arrange
            Mock<IRideRepository> mockRideRepository = new Mock<IRideRepository>();
            var expectedResult = new Ride();
            var methodUnderTest = mockRideRepository.Object;

            // Set-up mock repository
            mockRideRepository.Setup(m => m.GetSingleRide(It.IsAny<Int32>()))
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
        public void CreateRideViaRepository_ValidRide_CreateMethodCalledOnce()
        {
            // Arrange
            Mock<IRideRepository> mockRideRepository = new Mock<IRideRepository>();
            var methodUnderTest = mockRideRepository.Object;
            var newRide = new Ride
            {
                RideId = 3,
                PostId = 7,
                RideStatusId = 1,
                RequestId = 5
            };

            // Set-up mock repository
            mockRideRepository.Setup(m => m.CreateRide(newRide));
            mockRideRepository.Setup(m => m.Save());

            // Act
            methodUnderTest.CreateRide(newRide);
            methodUnderTest.Save();

            // Assert
            mockRideRepository.Verify(x => x.CreateRide(newRide), Times.Once());
        }

        [Fact]
        public void UpdateRideViaRepository_ChangeRideStatus_UpdateMethodCalledOnce()
        {
            // Arrange
            Mock<IRideRepository> mockRideRepository = new Mock<IRideRepository>();
            var methodUnderTest = mockRideRepository.Object;
            var updatedRide = new Ride
            {
                RideId = 3,
                PostId = 7,
                RideStatusId = 2,
                RequestId = 5
            };

            // Set-up mock repository
            mockRideRepository.Setup(m => m.UpdateRide(updatedRide));
            mockRideRepository.Setup(m => m.Save());

            // Act
            methodUnderTest.UpdateRide(updatedRide);
            methodUnderTest.Save();

            // Assert
            mockRideRepository.Verify(x => x.UpdateRide(updatedRide), Times.Once());
        }

        [Fact]
        public void DeleteRideViaRepository_ValidRideId_DeleteMethodCalledOnce()
        {
            // Arrange
            Mock<IRideRepository> mockRideRepository = new Mock<IRideRepository>();
            var methodUnderTest = mockRideRepository.Object;
            var ride = new Ride
            {
                RideId = 3,
                PostId = 7,
                RideStatusId = 2,
                RequestId = 5
            };

            // Set-up mock repository
            mockRideRepository.Setup(m => m.DeleteRide(ride.RideId));
            mockRideRepository.Setup(m => m.Save());

            // Act
            methodUnderTest.DeleteRide(ride.RideId);
            methodUnderTest.Save();

            // Assert
            mockRideRepository.Verify(x => x.DeleteRide(ride.RideId), Times.Once());
        }
    }
}
