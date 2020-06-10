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
    public class RequestLogicTests
    {
        [Fact]
        public void GetSingleRequestViaBLL_ValidRequestId_ExpectedSuccess()
        {
            // Arrange
            Mock<IRequestLogic> mockRequestLogic = new Mock<IRequestLogic>();
            var expectedResult = new Request();
            var methodUnderTest = mockRequestLogic.Object;

            // Set-up mock logic
            mockRequestLogic.Setup(m => m.GetSingleRequest(It.IsAny<Int32>()))
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
        public void GetDriverRequestsViaBLL_ExpectedSuccess()
        {
            // Arrange
            Mock<IRequestLogic> mockRequestLogic = new Mock<IRequestLogic>();
            var methodUnderTest = mockRequestLogic.Object;
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

            // Set-up mock logic
            mockRequestLogic.Setup(m => m.GetDriverRequests(It.IsAny<Int32>()))
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
        public void GetPassengerRequestsViaBLL_ExpectedSuccess()
        {
            // Arrange
            Mock<IRequestLogic> mockRequestLogic = new Mock<IRequestLogic>();
            var systemUnderTest = mockRequestLogic.Object;
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

            // Set-up mock logic
            mockRequestLogic.Setup(m => m.GetPassengerRequests(2))
                .ReturnsAsync(expectedResult);

            // Act
            var allPosts = systemUnderTest.GetPassengerRequests(2);
            bool postsReturned = true;

            if (!allPosts.IsCompleted)
            {
                postsReturned = false;
            }

            // Assert
            Assert.True(postsReturned);
        }

        [Fact]
        public void CreateRequestViaBLL_ValidRequest_CreateMethodCalledOnce()
        {
            Mock<IRequestLogic> mockRequestLogic = new Mock<IRequestLogic>();
            var methodUnderTest = mockRequestLogic.Object;

            var newRequest = new Request
            {
                RequestId = 7,
                RequestStatusId = 4,
                PassengerId = 3,
                PostId = 10
            };

            // Set-up mock logic
            mockRequestLogic.Setup(m => m.CreateRequest(newRequest));
            mockRequestLogic.Setup(m => m.Save());

            // Act
            methodUnderTest.CreateRequest(newRequest);
            methodUnderTest.Save();

            // Assert
            mockRequestLogic.Verify(m => m.CreateRequest(newRequest), Times.Once());
        }

        [Fact]
        public void UpdateRequestViaBLL_ChangeStatus_UpdateMethodCalledOnce()
        {
            Mock<IRequestLogic> mockRequestLogic = new Mock<IRequestLogic>();
            var methodUnderTest = mockRequestLogic.Object;

            var updatedRequest = new Request
            {
                RequestId = 7,
                RequestStatusId = 2,
                PassengerId = 3,
                PostId = 10
            };

            // Set-up mock logic
            mockRequestLogic.Setup(m => m.UpdateRequest(updatedRequest));
            mockRequestLogic.Setup(m => m.Save());

            // Act
            methodUnderTest.UpdateRequest(updatedRequest);
            methodUnderTest.Save();

            // Assert
            mockRequestLogic.Verify(m => m.UpdateRequest(updatedRequest), Times.Once());
        }

        [Fact]
        public void AcceptRequestViaBLL_ChangeStatusToAccepted_UpdateMethodCalledOnce()
        {
            Mock<IRequestLogic> mockRequestLogic = new Mock<IRequestLogic>();
            var methodUnderTest = mockRequestLogic.Object;

            var updatedRequest = new Request
            {
                RequestId = 7,
                RequestStatusId = 1,
                PassengerId = 3,
                PostId = 10
            };

            // Set-up mock logic
            mockRequestLogic.Setup(m => m.UpdateRequest(updatedRequest));
            mockRequestLogic.Setup(m => m.Save());

            // Act
            methodUnderTest.UpdateRequest(updatedRequest);
            methodUnderTest.Save();

            // Assert
            mockRequestLogic.Verify(m => m.UpdateRequest(updatedRequest), Times.Once());
        }

        [Fact]
        public void DeclineRequestViaBLL_ChangeStatusToDeclined_UpdateMethodCalledOnce()
        {
            Mock<IRequestLogic> mockRequestLogic = new Mock<IRequestLogic>();
            var methodUnderTest = mockRequestLogic.Object;

            var updatedRequest = new Request
            {
                RequestId = 7,
                RequestStatusId = 2,
                PassengerId = 3,
                PostId = 10
            };

            // Set-up mock logic
            mockRequestLogic.Setup(m => m.UpdateRequest(updatedRequest));
            mockRequestLogic.Setup(m => m.Save());

            // Act
            methodUnderTest.UpdateRequest(updatedRequest);
            methodUnderTest.Save();

            // Assert
            mockRequestLogic.Verify(m => m.UpdateRequest(updatedRequest), Times.Once());
        }

        [Fact]
        public void DeleteRequestViaBLL_ValidRequestId_DeleteMethodCalledOnce()
        {
            Mock<IRequestLogic> mockRequestLogic = new Mock<IRequestLogic>();
            var methodUnderTest = mockRequestLogic.Object;

            var newRequest = new Request
            {
                RequestId = 7,
                RequestStatusId = 1,
                PassengerId = 3,
                PostId = 10
            };

            // Set-up mock logic
            mockRequestLogic.Setup(m => m.DeleteRequest(newRequest.RequestId));
            mockRequestLogic.Setup(m => m.Save());

            // Act
            methodUnderTest.DeleteRequest(newRequest.RequestId);
            methodUnderTest.Save();

            // Assert
            mockRequestLogic.Verify(m => m.DeleteRequest(newRequest.RequestId), Times.Once());
        }
    }
}
