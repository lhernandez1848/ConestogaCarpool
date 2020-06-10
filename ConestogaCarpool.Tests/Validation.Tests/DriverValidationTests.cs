using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConestogaCarpool.Tests.Validation.Tests
{
    public class DriverValidationTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public void DriverExperienceValidation_NegativeNumbers_ExpectedFail(int testValue)
        {
            // Arrange
            bool result = true;

            // Act
            result = DriverValidation.DriverExperienceValidation(testValue);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(2)]
        public void DriverExperienceValidation_ValidYearsOfExperience_ExpectedSuccess(int testValue)
        {
            // Arrange
            bool result = true;

            // Act
            result = DriverValidation.DriverExperienceValidation(testValue);

            // Assert
            Assert.True(result);
        }
    }
}
