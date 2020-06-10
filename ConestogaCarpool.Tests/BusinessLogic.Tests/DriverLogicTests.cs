using ConestogaCarpool.BusinessLogic;
using ConestogaCarpool.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConestogaCarpool.Tests.BusinessLogic.Tests
{
    public class DriverLogicTests
    {
        [Fact]
        public void GetSingleDriverViaBLL_DriverIdIsNotNull_GetSingleDriverSuccess()
        {
            // Arrange 
            Mock<IDriverLogic> mockDriverLogic = new Mock<IDriverLogic>();
            var expectedResult = new Driver();
            var methodUnderTest = mockDriverLogic.Object;

            mockDriverLogic.Setup(m => m.GetSingleDriver(It.IsAny<Int32>()))
                .ReturnsAsync(expectedResult);

            // Act
            var driverReturned = methodUnderTest.GetSingleDriver(1);
            bool isDriverReturned;

            if (driverReturned == null)
                isDriverReturned = false;
            else
                isDriverReturned = true;

            // Assert
            Assert.True(isDriverReturned);

        }

        [Fact]
        public void IsUserDriverViaBLL_UserIdIsNotNull_ExpectedSuccess()
        {
            // Arrange
            Mock<IDriverLogic> mockDriverLogic = new Mock<IDriverLogic>();
            var expectedResult = new List<Driver>();
            expectedResult.Add(new Driver
            {
                DriverId = 1,
                UserId = 2,
                LicenceClassId = 1,
                Experience = 10
            });
            var methodUnderTest = mockDriverLogic.Object;

            // Set-up mock logic
            mockDriverLogic.Setup(m => m.GetDrivers(It.IsAny<Int32>()))
                .ReturnsAsync(expectedResult);

            // Act
            var driverReturned = methodUnderTest.GetDrivers(1);
            bool isUserDriver = true;

            if (driverReturned == null)
                isUserDriver = false;

            // Assert
            Assert.True(isUserDriver);
        }

        [Fact]
        public void GetDriversViaBLL_ExpectedSuccess()
        {
            // Arrange
            Mock<IDriverLogic> mockDriverLogic = new Mock<IDriverLogic>();
            var expectedResult = new List<Driver>();
            var methodUnderTest = mockDriverLogic.Object;
            expectedResult.Add(new Driver
            {
                DriverId = 1,
                UserId = 1,
                LicenceClassId = 1,
                Experience = 4
            });
            expectedResult.Add(new Driver
            {
                DriverId = 2,
                UserId = 6,
                LicenceClassId = 2,
                Experience = 2
            });

            // Set-up mock logic
            mockDriverLogic.Setup(m => m.GetDrivers(It.IsAny<Int32>()))
                .ReturnsAsync(expectedResult);

            // Act
            var allDrivers = methodUnderTest.GetDrivers(1);
            bool driversReturned = true;

            if (!allDrivers.IsCompleted)
            {
                driversReturned = false;
            }

            // Assert
            Assert.True(driversReturned);
        }

        [Fact]
        public void GetDriverViaBLL_ValidId_ReturnsDriverDetails()
        {
            // Arrange
            Mock<IDriverLogic> mockDriverLogic = new Mock<IDriverLogic>();
            var expectedResult = new Driver();
            var methodUnderTest = mockDriverLogic.Object;

            // Set-up mock logic
            mockDriverLogic.Setup(m => m.GetSingleDriver(It.IsAny<Int32>()))
                .ReturnsAsync(expectedResult);

            // Act
            var driverReturned = methodUnderTest.GetSingleDriver(1);
            bool isDriverReturned = true;

            if (driverReturned == null)
            {
                isDriverReturned = false;
            }

            // Assert
            Assert.True(isDriverReturned);
        }

        [Fact]
        public void CreateDriverViaBLL_ValidDriver_CreateMethodCalledOnce()
        {
            // Arrange
            Mock<IDriverLogic> mockDriverLogic = new Mock<IDriverLogic>();
            var methodUnderTest = mockDriverLogic.Object;
            var newDriver = new Driver
            {
                DriverId = 1,
                UserId = 1,
                LicenceClassId = 1,
                Experience = 4
            };

            // Set-up mock logic
            mockDriverLogic.Setup(m => m.CreateDriver(newDriver));
            mockDriverLogic.Setup(m => m.Save());

            // Act
            methodUnderTest.CreateDriver(newDriver);
            methodUnderTest.Save();

            // Assert
            mockDriverLogic.Verify(x => x.CreateDriver(newDriver), Times.Once());
        }

        [Fact]
        public void UpdateDriverViaBLL_ChangeExperience_UpdateMethodCalledOnce()
        {
            // Arrange
            Mock<IDriverLogic> mockDriverLogic = new Mock<IDriverLogic>();
            var methodUnderTest = mockDriverLogic.Object;
            var updatedDriver = new Driver
            {
                DriverId = 1,
                UserId = 1,
                LicenceClassId = 1,
                Experience = 5
            };

            // Set-up mock logic
            mockDriverLogic.Setup(m => m.UpdateDriver(updatedDriver));
            mockDriverLogic.Setup(m => m.Save());

            // Act
            methodUnderTest.UpdateDriver(updatedDriver);
            methodUnderTest.Save();

            // Assert
            mockDriverLogic.Verify(x => x.UpdateDriver(updatedDriver), Times.Once());
        }

        [Fact]
        public void DeleteDriverViaBLL_ValidDriverId_DeleteMethodCalledOnce()
        {
            // Arrange
            Mock<IDriverLogic> mockDriverLogic = new Mock<IDriverLogic>();
            var methodUnderTest = mockDriverLogic.Object;
            var driver = new Driver
            {
                DriverId = 1,
                UserId = 1,
                LicenceClassId = 1,
                Experience = 5
            };

            // Set-up mock logic
            mockDriverLogic.Setup(m => m.DeleteDriver(driver.DriverId));
            mockDriverLogic.Setup(m => m.Save());

            // Act
            methodUnderTest.DeleteDriver(driver.DriverId);
            methodUnderTest.Save();

            // Assert
            mockDriverLogic.Verify(x => x.DeleteDriver(driver.DriverId), Times.Once());
        }
    }
}
