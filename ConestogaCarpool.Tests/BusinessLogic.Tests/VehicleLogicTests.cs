using ConestogaCarpool.BusinessLogic;
using ConestogaCarpool.Models;
using ConestogaCarpool.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConestogaCarpool.Tests.BusinessLogic.Tests
{
    public class VehicleLogicTests
    {
        [Fact]
        public void GetSingleVehicleViaBLL_VehicleIdIsNotNull_GetSingleVehicleSuccess()
        {
            // Arrange 
            Mock<IVehicleLogic> mockVehicleLogic = new Mock<IVehicleLogic>();
            var expectedResult = new Vehicle();
            var methodUnderTest = mockVehicleLogic.Object;

            mockVehicleLogic.Setup(m => m.GetSingleVehicle(It.IsAny<Int32>()))
                .ReturnsAsync(expectedResult);

            // Act
            var vehicleReturned = methodUnderTest.GetSingleVehicle(1);
            bool isVehicleReturned;

            if (vehicleReturned == null)
            {
                isVehicleReturned = false;
            }

            else
            {
                isVehicleReturned = true;
            }

            // Assert
            Assert.True(isVehicleReturned);

        }

        [Fact]
        public void GetVehiclesOwnedViaBLL_UserIdIsNotNull_ExpectedSuccess()
        {
            // Arrange
            Mock<IVehicleLogic> mockVehicleLogic = new Mock<IVehicleLogic>();
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
            var methodUnderTest = mockVehicleLogic.Object;

            // Set-up mock logic
            mockVehicleLogic.Setup(m => m.GetVehiclesOwned(It.IsAny<Int32>()))
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
        public void CreateVehicleViaBLL_ValidVehicle_ExpectedCreateMethodCalledOnce()
        {
            // Arrange
            Mock<IVehicleLogic> mockVehicleLogic = new Mock<IVehicleLogic>();
            var methodUnderTest = mockVehicleLogic.Object;
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

            // Set-up mock logic
            mockVehicleLogic.Setup(v => v.CreateVehicle(vehicle));
            mockVehicleLogic.Setup(v => v.Save());

            // Act
            methodUnderTest.CreateVehicle(vehicle);
            methodUnderTest.Save();

            // Assert
            mockVehicleLogic.Verify(x => x.CreateVehicle(vehicle), Times.Once());
        }

        [Fact]
        public void UpdateVehicleViaBLL_ChangeVehicleColor_ExpectedUpdateMethodCalledOnce()
        {
            // Arrange
            Mock<IVehicleLogic> mockVehicleLogic = new Mock<IVehicleLogic>();
            var methodUnderTest = mockVehicleLogic.Object;
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

            // Set-up mock logic
            mockVehicleLogic.Setup(v => v.UpdateVehicle(updatedVehicle));
            mockVehicleLogic.Setup(v => v.Save());

            // Act
            methodUnderTest.UpdateVehicle(updatedVehicle);
            methodUnderTest.Save();

            // Assert
            mockVehicleLogic.Verify(x => x.UpdateVehicle(updatedVehicle), Times.Once());
        }

        [Fact]
        public void DeleteVehicleViaBLL_ValidVehicleId_ExpectedDeleteMethodCalledOnce()
        {
            // Arrange
            Mock<IVehicleLogic> mockVehicleLogic = new Mock<IVehicleLogic>();
            var methodUnderTest = mockVehicleLogic.Object;
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

            // Set-up mock logic
            mockVehicleLogic.Setup(m => m.DeleteVehicle(vehicle.VehicleId));
            mockVehicleLogic.Setup(m => m.Save());

            // Act
            methodUnderTest.DeleteVehicle(vehicle.VehicleId);
            methodUnderTest.Save();

            // Assert
            mockVehicleLogic.Verify(x => x.DeleteVehicle(vehicle.VehicleId), Times.Once());
        }
    }
}
