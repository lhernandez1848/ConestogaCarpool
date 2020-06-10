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
    public class UserLogicTests
    {
        [Fact]
        public void GetUserViaBLL_UserIdIsNotNull_GetUserSuccess()
        {
            // Arrange
            Mock<IUserLogic> mockUserLogic = new Mock<IUserLogic>();
            var expectedResult = new User();
            var methodUnderTest = mockUserLogic.Object;

            // Set-up mock repository
            mockUserLogic.Setup(m => m.GetUser(It.IsAny<Int32>()))
                .ReturnsAsync(expectedResult);

            // Act
            var userReturned = methodUnderTest.GetUser(1);
            bool isUserReturned = true;

            if (userReturned == null)
            {
                isUserReturned = false;
            }

            // Assert
            Assert.True(isUserReturned);
        }

        [Fact]
        public void GetSingleUserViaBLL_ValidUsername_GetUserSuccess()
        {
            // Arrange
            Mock<IUserLogic> mockUserLogic = new Mock<IUserLogic>();
            var expectedResult = new List<User>();
            var methodUnderTest = mockUserLogic.Object;

            // Set-up mock repository
            mockUserLogic.Setup(m => m.GetSingleUser("aread"))
                .ReturnsAsync(expectedResult);

            // Act
            var userReturned = methodUnderTest.GetSingleUser("aread");
            bool isUserReturned = true;

            if (userReturned == null)
            {
                isUserReturned = false;
            }

            // Assert
            Assert.True(isUserReturned);
        }

        [Fact]
        public void FindUserByIdViaBLL_ValidUserId_GetUserSuccess()
        {
            // Arrange
            Mock<IUserLogic> mockUserLogic = new Mock<IUserLogic>();
            var expectedResult = new User();
            var methodUnderTest = mockUserLogic.Object;

            // Set-up mock repository
            mockUserLogic.Setup(m => m.FindUserById(It.IsAny<Int32>()))
                .ReturnsAsync(expectedResult);

            // Act
            var userReturned = methodUnderTest.FindUserById(1);
            bool isUserReturned = true;

            if (userReturned == null)
            {
                isUserReturned = false;
            }

            // Assert
            Assert.True(isUserReturned);
        }

        [Fact]
        public void FindUserByEmailViaBLL_ValidUserId_GetUserSuccess()
        {
            // Arrange
            Mock<IUserLogic> mockUserLogic = new Mock<IUserLogic>();
            var expectedResult = new User();
            var methodUnderTest = mockUserLogic.Object;

            // Set-up mock repository
            mockUserLogic.Setup(m => m.FindUserByEmail("aread@conestogac.on.ca"))
                .ReturnsAsync(expectedResult);

            // Act
            var userReturned = methodUnderTest.FindUserByEmail("aread@conestogac.on.ca");
            bool isUserReturned = true;

            if (userReturned == null)
            {
                isUserReturned = false;
            }

            // Assert
            Assert.True(isUserReturned);
        }

        [Fact]
        public void UserLoginViaBLL_UserExists_ExpectedSuccess()
        {
            // Arrange
            Mock<IUserLogic> mockUserLogic = new Mock<IUserLogic>();

            string username = "cameron";
            string password = "paSSword";

            var methodUnderTest = mockUserLogic.Object;

            // Set-up mock repository
            mockUserLogic.Setup(m => m.UserLogin(username, password))
                .Returns(false);

            // Act
            var userReturned = methodUnderTest.UserLogin("cameron", "paSSword");
            bool userExists = true;

            if (userReturned == false)
            {
                userExists = false;
            }

            // Assert
            Assert.False(userExists);
        }

        [Fact]
        public void CreateUserViaBLL_ValidUser_CreateMethodCalledOnce()
        {
            // Arrange
            Mock<IUserLogic> mockUserLogic = new Mock<IUserLogic>();
            var methodUnderTest = mockUserLogic.Object;
            var newUser = new User
            {
                UserId = 1,
                Username = "aread",
                Password = "aread123",
                FirstName = "Arthur",
                LastName = "Read",
                Email = "aread@conestogac.on.ca",
                VerifiedEmail = null
            };

            // Set-up mock repository
            mockUserLogic.Setup(m => m.CreateUser(newUser));
            mockUserLogic.Setup(m => m.Save());

            // Act
            methodUnderTest.CreateUser(newUser);
            methodUnderTest.Save();

            // Assert
            mockUserLogic.Verify(x => x.CreateUser(newUser), Times.Once());
        }

        [Fact]
        public void UpdateUserViaBLL_ChangeUsername_UpdateMethodCalledOnce()
        {
            // Arrange
            Mock<IUserLogic> mockUserLogic = new Mock<IUserLogic>();
            var methodUnderTest = mockUserLogic.Object;
            var updatedUser = new User
            {
                UserId = 1,
                Username = "iamarthur",
                Password = "aread123",
                FirstName = "Arthur",
                LastName = "Read",
                Email = "aread@conestogac.on.ca",
                VerifiedEmail = null
            };

            // Set-up mock repository
            mockUserLogic.Setup(m => m.UpdateUser(updatedUser));
            mockUserLogic.Setup(m => m.Save());

            // Act
            methodUnderTest.UpdateUser(updatedUser);
            methodUnderTest.Save();

            // Assert
            mockUserLogic.Verify(x => x.UpdateUser(updatedUser), Times.Once());
        }

        [Fact]
        public void UpdateEmailVerificationViaBLL_ChangeVerifiedEmail_UpdateMethodCalledOnce()
        {
            // Arrange
            Mock<IUserLogic> mockUserLogic = new Mock<IUserLogic>();
            var methodUnderTest = mockUserLogic.Object;
            var updatedUser = new User
            {
                UserId = 1,
                Username = "iamarthur",
                Password = "aread123",
                FirstName = "Arthur",
                LastName = "Read",
                Email = "aread@conestogac.on.ca",
                VerifiedEmail = "yes"
            };

            // Set-up mock repository
            mockUserLogic.Setup(m => m.UpdateUser(updatedUser));
            mockUserLogic.Setup(m => m.Save());

            // Act
            methodUnderTest.UpdateUser(updatedUser);
            methodUnderTest.Save();

            // Assert
            mockUserLogic.Verify(x => x.UpdateUser(updatedUser), Times.Once());
        }

        [Fact]
        public void DeleteUserViaBLL_ValidUserId_DeleteMethodCalledOnce()
        {
            // Arrange
            Mock<IUserLogic> mockUserLogic = new Mock<IUserLogic>();
            var methodUnderTest = mockUserLogic.Object;
            var user = new User
            {
                UserId = 1,
                Username = "iamarthur",
                Password = "aread123",
                FirstName = "Arthur",
                LastName = "Read",
                Email = "aread@conestogac.on.ca",
                VerifiedEmail = "yes"
            };

            // Set-up mock repository
            mockUserLogic.Setup(m => m.DeleteUser(user.UserId));
            mockUserLogic.Setup(m => m.Save());

            // Act
            methodUnderTest.DeleteUser(user.UserId);
            methodUnderTest.Save();

            // Assert
            mockUserLogic.Verify(x => x.DeleteUser(user.UserId), Times.Once());
        }
    }
}
