using ConestogaCarpool.Models;
using ConestogaCarpool.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConestogaCarpool.Tests.Repositories.Tests
{
    public class RequestRepositoryTests
    {
        [Fact]
        public void GetSingleRequestViaRepository_ValidRequestId_ExpectedSuccess()
        {
            // Arrange
            Mock<IRequestRepository> mockRequestRepository = new Mock<IRequestRepository>();
            var expectedResult = new Request();
            var methodUnderTest = mockRequestRepository.Object;

            // Set-up mock repository
            mockRequestRepository.Setup(m => m.GetSingleRequest(It.IsAny<Int32>()))
                .ReturnsAsync(expectedResult);

            // Act
            var postFound = methodUnderTest.GetSingleRequest(1);
            bool isPostFound = true;

            if (postFound == null)
            {
                isPostFound = false;
            }

            // Assert
            Assert.True(isPostFound);
        }

        [Fact]
        public void GetDriverRequestsViaRepository_ExpectedSuccess()
        {
            // Arrange
            Mock<IRequestRepository> mockRequestRepository = new Mock<IRequestRepository>();
            var expectedResult = new List<Request>();
            expectedResult.Add(new Request
            {
                RequestId = 1,
                RequestStatusId = 1,
                PassengerId = 2,
                PostId = 1
            });
            expectedResult.Add(new Request
            {
                RequestId = 2,
                RequestStatusId = 1,
                PassengerId = 3,
                PostId = 2
            });
            var methodUnderTest = mockRequestRepository.Object;

            // Set-up mock repository
            mockRequestRepository.Setup(m => m.GetDriverRequests(It.IsAny<Int32>()))
                .ReturnsAsync(expectedResult);

            // Act
            var allPosts = methodUnderTest.GetDriverRequests(1);
            bool postsReturned = true;

            if (!allPosts.IsCompleted)
            {
                postsReturned = false;
            }

            // Assert
            Assert.True(postsReturned);
        }

        [Fact]
        public void GetPassengerRequestsViaRepository_ExpectedSuccess()
        {
            // Arrange
            Mock<IRequestRepository> mockRequestRepository = new Mock<IRequestRepository>();
            var expectedResult = new List<Request>();
            expectedResult.Add(new Request
            {
                RequestId = 5,
                RequestStatusId = 1,
                PassengerId = 2,
                PostId = 9
            });
            expectedResult.Add(new Request
            {
                RequestId = 6,
                RequestStatusId = 1,
                PassengerId = 2,
                PostId = 8
            });
            var methodUnderTest = mockRequestRepository.Object;

            // Set-up mock repository
            mockRequestRepository.Setup(m => m.GetPassengerRequests(2))
                .ReturnsAsync(expectedResult);

            // Act
            var allPosts = methodUnderTest.GetPassengerRequests(2);
            bool postsReturned = true;

            if (!allPosts.IsCompleted)
            {
                postsReturned = false;
            }

            // Assert
            Assert.True(postsReturned);
        }

        [Fact]
        public void CreateRequestViaRepository_ValidRequest_CreateMethodCalledOnce()
        {
            Mock<IRequestRepository> mockRequestRepository = new Mock<IRequestRepository>();
            var methodUnderTest = mockRequestRepository.Object;

            var newRequest = new Request
            {
                RequestId = 7,
                RequestStatusId = 4,
                PassengerId = 3,
                PostId = 10
            };

            // Set-up mock repository
            mockRequestRepository.Setup(m => m.CreateRequest(newRequest));
            mockRequestRepository.Setup(m => m.Save());

            // Act
            methodUnderTest.CreateRequest(newRequest);
            methodUnderTest.Save();

            // Assert
            mockRequestRepository.Verify(m => m.CreateRequest(newRequest), Times.Once());
        }

        [Fact]
        public void UpdateRequestViaRepository_ChangeStatus_UpdateMethodCalledOnce()
        {
            Mock<IRequestRepository> mockRequestRepository = new Mock<IRequestRepository>();
            var methodUnderTest = mockRequestRepository.Object;

            var updatedRequest = new Request
            {
                RequestId = 7,
                RequestStatusId = 2,
                PassengerId = 3,
                PostId = 10
            };

            // Set-up mock repository
            mockRequestRepository.Setup(m => m.UpdateRequest(updatedRequest));
            mockRequestRepository.Setup(m => m.Save());

            // Act
            methodUnderTest.UpdateRequest(updatedRequest);
            methodUnderTest.Save();

            // Assert
            mockRequestRepository.Verify(m => m.UpdateRequest(updatedRequest), Times.Once());
        }

        [Fact]
        public void AcceptRequestViaRepository_ChangeStatusToAccepted_UpdateMethodCalledOnce()
        {
            Mock<IRequestRepository> mockRequestRepository = new Mock<IRequestRepository>();
            var methodUnderTest = mockRequestRepository.Object;

            var updatedRequest = new Request
            {
                RequestId = 7,
                RequestStatusId = 1,
                PassengerId = 3,
                PostId = 10
            };

            // Set-up mock repository
            mockRequestRepository.Setup(m => m.UpdateRequest(updatedRequest));
            mockRequestRepository.Setup(m => m.Save());

            // Act
            methodUnderTest.UpdateRequest(updatedRequest);
            methodUnderTest.Save();

            // Assert
            mockRequestRepository.Verify(m => m.UpdateRequest(updatedRequest), Times.Once());
        }

        [Fact]
        public void DeclineRequestViaRepository_ChangeStatusToDeclined_UpdateMethodCalledOnce()
        {
            Mock<IRequestRepository> mockRequestRepository = new Mock<IRequestRepository>();
            var methodUnderTest = mockRequestRepository.Object;

            var updatedRequest = new Request
            {
                RequestId = 7,
                RequestStatusId = 2,
                PassengerId = 3,
                PostId = 10
            };

            // Set-up mock repository
            mockRequestRepository.Setup(m => m.UpdateRequest(updatedRequest));
            mockRequestRepository.Setup(m => m.Save());

            // Act
            methodUnderTest.UpdateRequest(updatedRequest);
            methodUnderTest.Save();

            // Assert
            mockRequestRepository.Verify(m => m.UpdateRequest(updatedRequest), Times.Once());
        }

        [Fact]
        public void DeleteRequestViaRepository_ValidRequestId_DeleteMethodCalledOnce()
        {
            Mock<IRequestRepository> mockRequestRepository = new Mock<IRequestRepository>();
            var methodUnderTest = mockRequestRepository.Object;

            var newRequest = new Request
            {
                RequestId = 7,
                RequestStatusId = 1,
                PassengerId = 3,
                PostId = 10
            };

            // Set-up mock repository
            mockRequestRepository.Setup(m => m.DeleteRequest(newRequest.RequestId));
            mockRequestRepository.Setup(m => m.Save());

            // Act
            methodUnderTest.DeleteRequest(newRequest.RequestId);
            methodUnderTest.Save();

            // Assert
            mockRequestRepository.Verify(m => m.DeleteRequest(newRequest.RequestId), Times.Once());
        }
    }
}
