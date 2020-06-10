using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConestogaCarpool.Tests.Validation.Tests
{
    public class UserValidationTests
    {
        [Theory]
        [InlineData("aread123@gmail.com")]
        [InlineData("thescentist@yahoo.com")]
        [InlineData("bjh329@redvelvet.smtown.com")]
        public void UserEmailValidation_InvalidEmailAddressess_ExpectedFail(string testValue)
        {
            // Arrange
            bool result = true;

            // Act
            result = UserValidation.UserEmailValidation(testValue);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("aread@conestogac.on.ca")]
        [InlineData("narmstrong1548@conestogac.on.ca")]
        [InlineData("rMcDonald@conestogac.on.ca")]
        public void UserEmailValidation_ConestogaEmailAddresses_ExpectedSuccess(string testValue)
        {
            // Arrange
            bool result = true;

            // Act
            result = UserValidation.UserEmailValidation(testValue);

            // Assert
            Assert.True(result);
        }
    }
}
