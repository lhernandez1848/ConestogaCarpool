using ConestogaCarpool.Models;
using ConestogaCarpool.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ConestogaCarpool.Tests.Repositories.Tests
{
    public class VehicleRepositoryTests
    {
        [Fact]
        public void GetSingleVehicleViaRepository_VehicleIdIsNotNull_GetSingleVehicleSuccess()
        {
            // Arrange
            Mock<IVehicleRepository> mockVehicleRepository = new Mock<IVehicleRepository>();
            var expectedResult = new Vehicle();
            var methodUnderTest = mockVehicleRepository.Object;

            // Set-up mock repository
            mockVehicleRepository.Setup(m => m.GetSingleVehicle(It.IsAny<Int32>()))
                .ReturnsAsync(expectedResult);

            // Act
            var vehicleReturned = methodUnderTest.GetSingleVehicle(1);
            bool isVehicleReturned = true;

            if (vehicleReturned == null)
            {
                isVehicleReturned = false;
            }

            // Assert
            Assert.True(isVehicleReturned);
        }

        [Fact]
        public void GetVehiclesOwnedViaRepository_UserIdIsNotNull_ExpectedSuccess()
        {
            // Arrange
            Mock<IVehicleRepository> mockVehicleRepository = new Mock<IVehicleRepository>();
            var expectedResult = new List<Vehicle>();
            expectedResult.Add(new Vehicle
            {
                VehicleId = 1,
                UserId = 1,
                Make = "Chevrolet",
                Model = "Cruze",
                Year = 2017,
                Colour = "Black",
                Plate = "ABCD123"
            });
            var methodUnderTest = mockVehicleRepository.Object;

            // Set-up mock repository
            mockVehicleRepository.Setup(m => m.GetVehiclesOwned(It.IsAny<Int32>()))
                .ReturnsAsync(expectedResult);

            // Act
            var vehicleReturned = methodUnderTest.GetVehiclesOwned(1);
            bool isVehicleReturned = true;

            if (vehicleReturned == null)
            {
                isVehicleReturned = false;
            }

            // Assert
            Assert.True(isVehicleReturned);
        }

        [Fact]
        public void CreateVehicleViaRepository_ValidVehicle_ExpectedCreateMethodCalledOnce()
        {
            // Arrange
            Mock<IVehicleRepository> mockVehicleRepository = new Mock<IVehicleRepository>();
            var methodUnderTest = mockVehicleRepository.Object;
            var vehicle = new Vehicle
            {
                VehicleId = 1,
                UserId = 1,
                Make = "Chevrolet",
                Model = "Cruze",
                Year = 2017,
                Colour = "Black",
                Plate = "ABCD123"
            };

            // Set-up mock repository
            mockVehicleRepository.Setup(v => v.CreateVehicle(vehicle));
            mockVehicleRepository.Setup(v => v.Save());

            // Act
            methodUnderTest.CreateVehicle(vehicle);
            methodUnderTest.Save();

            // Assert
            mockVehicleRepository.Verify(x => x.CreateVehicle(vehicle), Times.Once());
        }

        [Fact]
        public void UpdateVehicleViaRepository_ChangeVehicleColor_ExpectedUpdateMethodCalledOnce()
        {
            // Arrange
            Mock<IVehicleRepository> mockVehicleRepository = new Mock<IVehicleRepository>();
            var methodUnderTest = mockVehicleRepository.Object;
            var updatedVehicle = new Vehicle
            {
                VehicleId = 1,
                UserId = 1,
                Make = "Chevrolet",
                Model = "Cruze",
                Year = 2017,
                Colour = "Silver",
                Plate = "ABCD123"
            };

            // Set-up mock repository
            mockVehicleRepository.Setup(v => v.UpdateVehicle(updatedVehicle));
            mockVehicleRepository.Setup(v => v.Save());

            // Act
            methodUnderTest.UpdateVehicle(updatedVehicle);
            methodUnderTest.Save();

            // Assert
            mockVehicleRepository.Verify(x => x.UpdateVehicle(updatedVehicle), Times.Once());
        }

        [Fact]
        public void DeleteVehicleViaRepository_ValidVehicleId_ExpectedDeleteMethodCalledOnce()
        {
            // Arrange
            Mock<IVehicleRepository> mockVehicleRepository = new Mock<IVehicleRepository>();
            var methodUnderTest = mockVehicleRepository.Object;
            var vehicle = new Vehicle
            {
                VehicleId = 1,
                UserId = 1,
                Make = "Chevrolet",
                Model = "Cruze",
                Year = 2017,
                Colour = "Black",
                Plate = "ABCD123"
            };

            // Set-up mock repository
            mockVehicleRepository.Setup(m => m.DeleteVehicle(vehicle.VehicleId));
            mockVehicleRepository.Setup(m => m.Save());

            // Act
            methodUnderTest.DeleteVehicle(vehicle.VehicleId);
            methodUnderTest.Save();

            // Assert
            mockVehicleRepository.Verify(x => x.DeleteVehicle(vehicle.VehicleId), Times.Once());
        }
    }
}
