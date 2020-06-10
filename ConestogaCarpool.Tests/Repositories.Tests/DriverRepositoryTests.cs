using ConestogaCarpool.Models;
using ConestogaCarpool.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConestogaCarpool.Tests.Repositories.Tests
{
    public class DriverRepositoryTests
    {
        [Fact]
        public void GetSingleDriverViaRepository_ValidDriverId_ReturnsDriverDetails()
        {
            // Arrange
            Mock<IDriverRepository> mockDriverRepository = new Mock<IDriverRepository>();
            var expectedResult = new Driver();
            var methodUnderTest = mockDriverRepository.Object;

            // Set-up mock repository
            mockDriverRepository.Setup(m => m.GetSingleDriver(It.IsAny<Int32>()))
                .ReturnsAsync(expectedResult);

            // Act
            var driverReturned = methodUnderTest.GetSingleDriver(0);
            bool isDriverReturned = true;

            if (driverReturned == null)
            {
                isDriverReturned = false;
            }

            // Assert
            Assert.True(isDriverReturned);
        }


        [Fact]
        public void IsUserDriverViaRepository_UserIdIsNull_ExpectedFails()
        {
            // Arrange
            Mock<IDriverRepository> mockDriverRepository = new Mock<IDriverRepository>();
            var expectedResult = false;
            var systemUnderTest = mockDriverRepository.Object;

            // Set-up mock repository
            mockDriverRepository.Setup(m => m.IsUserDriver(null))
                .Returns(expectedResult);

            // Act
            var driverReturned = systemUnderTest.IsUserDriver(null);

            // Assert
            Assert.False(driverReturned);
        }

        [Fact]
        public void GetDriversViaRepository_ExpectedSuccess()
        {
            // Arrange
            Mock<IDriverRepository> mockDriverRepository = new Mock<IDriverRepository>();
            var expectedResult = new List<Driver>();
            var methodUnderTest = mockDriverRepository.Object;
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

            // Set-up mock repository
            mockDriverRepository.Setup(m => m.GetDrivers(It.IsAny<Int32>()))
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
        public void GetDriverViaRepository_ValidId_ReturnsDriverDetails()
        {
            // Arrange
            Mock<IDriverRepository> mockDriverRepository = new Mock<IDriverRepository>();
            var expectedResult = new Driver();
            var methodUnderTest = mockDriverRepository.Object;

            // Set-up mock repository
            mockDriverRepository.Setup(m => m.GetSingleDriver(It.IsAny<Int32>()))
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
        public void CreateDriverViaRepository_ValidDriver_CreateMethodCalledOnce()
        {
            // Arrange
            Mock<IDriverRepository> mockDriverRepository = new Mock<IDriverRepository>();
            var methodUnderTest = mockDriverRepository.Object;
            var newDriver = new Driver
            {
                DriverId = 1,
                UserId = 1,
                LicenceClassId = 1,
                Experience = 4
            };

            // Set-up mock repository
            mockDriverRepository.Setup(m => m.CreateDriver(newDriver));
            mockDriverRepository.Setup(m => m.Save());

            // Act
            methodUnderTest.CreateDriver(newDriver);
            methodUnderTest.Save();

            // Assert
            mockDriverRepository.Verify(x => x.CreateDriver(newDriver), Times.Once());
        }

        [Fact]
        public void UpdateDriverViaRepository_ChangeExperience_UpdateMethodCalledOnce()
        {
            // Arrange
            Mock<IDriverRepository> mockDriverRepository = new Mock<IDriverRepository>();
            var methodUnderTest = mockDriverRepository.Object;
            var updatedDriver = new Driver
            {
                DriverId = 1,
                UserId = 1,
                LicenceClassId = 1,
                Experience = 5
            };

            // Set-up mock repository
            mockDriverRepository.Setup(m => m.UpdateDriver(updatedDriver));
            mockDriverRepository.Setup(m => m.Save());

            // Act
            methodUnderTest.UpdateDriver(updatedDriver);
            methodUnderTest.Save();

            // Assert
            mockDriverRepository.Verify(x => x.UpdateDriver(updatedDriver), Times.Once());
        }

        [Fact]
        public void DeleteDriverViaRepository_ValidDriverId_DeleteMethodCalledOnce()
        {
            // Arrange
            Mock<IDriverRepository> mockDriverRepository = new Mock<IDriverRepository>();
            var methodUnderTest = mockDriverRepository.Object;
            var driver = new Driver
            {
                DriverId = 1,
                UserId = 1,
                LicenceClassId = 1,
                Experience = 5
            };

            // Set-up mock repository
            mockDriverRepository.Setup(m => m.DeleteDriver(driver.DriverId));
            mockDriverRepository.Setup(m => m.Save());

            // Act
            methodUnderTest.DeleteDriver(driver.DriverId);
            methodUnderTest.Save();

            // Assert
            mockDriverRepository.Verify(x => x.DeleteDriver(driver.DriverId), Times.Once());
        }
    }
}
