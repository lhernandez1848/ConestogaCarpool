using ConestogaCarpool.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConestogaCarpool.Tests.Validation.Tests
{
    public class PostValidationTests
    {
        [Fact]
        public void PostDateValidation_YesterdaysDate_ExpectedFail()
        {
            // Arrange
            bool result = true;
            DateTime testDate = DateTime.Now.AddDays(-1);

            // Act
            result = PostValidation.PostDateValidation(testDate);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void PostDateValidation_TomorrowsDate_ExpectedFail()
        {
            // Arrange
            bool result = true;
            DateTime testDate = DateTime.Now.AddDays(1);

            // Act
            result = PostValidation.PostDateValidation(testDate);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void PostDateValidation_FutureDate_ExpectedFail()
        {
            // Arrange
            bool result = true;
            DateTime testDate = DateTime.Now.AddDays(7);

            // Act
            result = PostValidation.PostDateValidation(testDate);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void PostDateValidation_ValidDate_ExpectedSuccess()
        {
            // Arrange
            bool result = true;
            DateTime testDate = DateTime.Today;

            // Act
            result = PostValidation.PostDateValidation(testDate);

            // Assert
            Assert.True(result);
        }
    }
}
