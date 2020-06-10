using ConestogaCarpool.Models;
using ConestogaCarpool.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConestogaCarpool.Tests.Repositories.Tests
{
    public class UserRepositoryTests
    {
        [Fact]
        public void GetUserViaRepository_UserIdIsNotNull_GetUserSuccess()
        {
            // Arrange
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            var expectedResult = new User();
            var methodUnderTest = mockUserRepository.Object;

            // Set-up mock repository
            mockUserRepository.Setup(m => m.GetUser(It.IsAny<Int32>()))
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
        public void GetSingleUserViaRepository_ValidUsername_GetUserSuccess()
        {
            // Arrange
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            var expectedResult = new List<User>();
            var methodUnderTest = mockUserRepository.Object;

            // Set-up mock repository
            mockUserRepository.Setup(m => m.GetSingleUser("aread"))
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
        public void FindUserByIdViaRepository_ValidUserId_GetUserSuccess()
        {
            // Arrange
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            var expectedResult = new User();
            var methodUnderTest = mockUserRepository.Object;

            // Set-up mock repository
            mockUserRepository.Setup(m => m.FindUserById(It.IsAny<Int32>()))
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
        public void FindUserByEmailViaRepository_ValidUserId_GetUserSuccess()
        {
            // Arrange
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            var expectedResult = new User();
            var methodUnderTest = mockUserRepository.Object;

            // Set-up mock repository
            mockUserRepository.Setup(m => m.FindUserByEmail("aread@conestogac.on.ca"))
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
        public void UserLoginViaRepository_UserExists_ExpectedSuccess()
        {
            // Arrange
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();

            string username = "cameron";
            string password = "paSSword";

            var methodUnderTest = mockUserRepository.Object;

            // Set-up mock repository
            mockUserRepository.Setup(m => m.UserLogin(username, password))
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
        public void CreateUserViaRepository_ValidUser_CreateMethodCalledOnce()
        {
            // Arrange
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            var methodUnderTest = mockUserRepository.Object;
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
            mockUserRepository.Setup(m => m.CreateUser(newUser));
            mockUserRepository.Setup(m => m.Save());

            // Act
            methodUnderTest.CreateUser(newUser);
            methodUnderTest.Save();

            // Assert
            mockUserRepository.Verify(x => x.CreateUser(newUser), Times.Once());
        }

        [Fact]
        public void UpdateUserViaRepository_ChangeUsername_UpdateMethodCalledOnce()
        {
            // Arrange
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            var methodUnderTest = mockUserRepository.Object;
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
            mockUserRepository.Setup(m => m.UpdateUser(updatedUser));
            mockUserRepository.Setup(m => m.Save());

            // Act
            methodUnderTest.UpdateUser(updatedUser);
            methodUnderTest.Save();

            // Assert
            mockUserRepository.Verify(x => x.UpdateUser(updatedUser), Times.Once());
        }

        [Fact]
        public void UpdateEmailVerificationViaRepository_ChangeVerifiedEmail_UpdateMethodCalledOnce()
        {
            // Arrange
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            var methodUnderTest = mockUserRepository.Object;
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
            mockUserRepository.Setup(m => m.UpdateUser(updatedUser));
            mockUserRepository.Setup(m => m.Save());

            // Act
            methodUnderTest.UpdateUser(updatedUser);
            methodUnderTest.Save();

            // Assert
            mockUserRepository.Verify(x => x.UpdateUser(updatedUser), Times.Once());
        }

        [Fact]
        public void DeleteUserViaRepository_ValidUserId_DeleteMethodCalledOnce()
        {
            // Arrange
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            var methodUnderTest = mockUserRepository.Object;
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
            mockUserRepository.Setup(m => m.DeleteUser(user.UserId));
            mockUserRepository.Setup(m => m.Save());

            // Act
            methodUnderTest.DeleteUser(user.UserId);
            methodUnderTest.Save();

            // Assert
            mockUserRepository.Verify(x => x.DeleteUser(user.UserId), Times.Once());
        }
    }
}
