using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConestogaCarpool.Tests.Validation.Tests
{
    public class VehicleValidationTests
    {
        [Theory]
        [InlineData(1899)]
        [InlineData(1521)]
        [InlineData(1785)]
        [InlineData(2022)]
        [InlineData(2050)]
        public void VehicleYearValidation_InvalidYears_ExpectedFail(int testValue)
        {
            // Arrange
            bool result = true;

            // Act
            result = VehicleValidation.VehicleYearValidation(testValue);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(1999)]
        [InlineData(2003)]
        [InlineData(2017)]
        [InlineData(2018)]
        [InlineData(2019)]
        public void VehicleYearValidation_ValidYears_ExpectedSuccess(int testValue)
        {
            // Arrange
            bool result = true;

            // Act
            result = VehicleValidation.VehicleYearValidation(testValue);

            // Assert
            Assert.True(result);
        }
    }
}
