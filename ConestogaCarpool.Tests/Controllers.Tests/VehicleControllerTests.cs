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
    public class VehicleControllerTests
    {
        private readonly ConestogaCarpoolContext _context;

        [Fact]
        public void VehicleControllerListVehiclesReturnsView_ExpectedSuccess()
        {
            // Arrange
            var _mock = new Mock<IVehicleLogic>();
            _mock.Setup(x => x.GetVehiclesOwned(It.IsAny<Int32>())).ReturnsAsync(GetVehicles());
            var controllerUnderTest = new VehicleController(_context, _mock.Object);

            // Act
            var result = controllerUnderTest.ListVehicles(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(Task<IActionResult>), result.GetType());
        }

        [Fact]
        public void VehicleControllerSelectVehicleReturnsView_ExpectedSuccess()
        {
            // Arrange
            var _mock = new Mock<IVehicleLogic>();
            _mock.Setup(x => x.GetVehiclesOwned(It.IsAny<Int32>())).ReturnsAsync(GetVehicles());
            var controllerUnderTest = new VehicleController(_context, _mock.Object);

            // Act
            var result = controllerUnderTest.SelectVehicle(1, 1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(Task<IActionResult>), result.GetType());
        }

        [Fact]
        public void VehicleControllerCreateReturnsView_ExpectedSuccess()
        {
            // Arrange
            Mock<IVehicleLogic> mockVehicleLogic = new Mock<IVehicleLogic>();
            mockVehicleLogic.Setup(x => x.GetVehiclesOwned(It.IsAny<Int32>())).ReturnsAsync(GetVehicles());
            var controllerUnderTest = new VehicleController(_context, mockVehicleLogic.Object);

            var model = new Vehicle();
            controllerUnderTest.ModelState.AddModelError("error", "Invalid post.");

            // Act
            var result = controllerUnderTest.Create(model);

            // Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void VehicleControllerEditReturnsView_ExpectedSuccess()
        {
            // Arrange
            Vehicle vehicle = new Vehicle
            {
                VehicleId = 2,
                UserId = 2,
                Make = "Honda",
                Model = "Civic",
                Year = 2018,
                Colour = "Black",
                Plate = "BVCX357"
            };

            Mock<IVehicleLogic> mockVehicleLogic = new Mock<IVehicleLogic>();
            mockVehicleLogic.Setup(x => x.GetSingleVehicle(It.IsAny<Int32>())).ReturnsAsync(GetVehicle());
            var controllerUnderTest = new VehicleController(_context, mockVehicleLogic.Object);

            // Act
            var result = controllerUnderTest.Edit(1, vehicle);

            // Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void VehicleControllerDetailsReturnsView_ExpectedSuccess()
        {
            // Arrange
            Mock<IVehicleLogic> mockVehicleLogic = new Mock<IVehicleLogic>();
            mockVehicleLogic.Setup(x => x.GetSingleVehicle(It.IsAny<Int32>())).ReturnsAsync(GetVehicle());
            var controllerUnderTest = new VehicleController(_context, mockVehicleLogic.Object);

            // Act
            var result = controllerUnderTest.Details(1);

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void VehicleControllerDeleteConfirmedReturnsView_ExpectedSuccess()
        {
            // Arrange
            Mock<IVehicleLogic> mockVehicleLogic = new Mock<IVehicleLogic>();
            mockVehicleLogic.Setup(x => x.GetSingleVehicle(It.IsAny<Int32>())).ReturnsAsync(GetVehicle());
            var controllerUnderTest = new VehicleController(_context, mockVehicleLogic.Object);

            // Act
            var result = controllerUnderTest.DeleteConfirmed(1);

            // Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        #region Private Methods
        private List<Vehicle> GetVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            vehicles.Add(new Vehicle
            {
                VehicleId = 1,
                UserId = 1,
                Make = "Chevrolet",
                Model = "Cruze",
                Year = 2017,
                Colour = "Silver",
                Plate = "ABCD123"
            });
            vehicles.Add(new Vehicle
            {
                VehicleId = 2,
                UserId = 2,
                Make = "Honda",
                Model = "Civic",
                Year = 2018,
                Colour = "Black",
                Plate = "BVCX357"
            });

            return vehicles;
        }

        private Vehicle GetVehicle()
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            Vehicle newVehicle = new Vehicle
            {
                VehicleId = 1,
                UserId = 1,
                Make = "Chevrolet",
                Model = "Cruze",
                Year = 2017,
                Colour = "Silver",
                Plate = "ABCD123"
            };

            return newVehicle;
        }

        #endregion
    }
}
