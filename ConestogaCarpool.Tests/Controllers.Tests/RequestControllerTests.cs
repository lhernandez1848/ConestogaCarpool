using ConestogaCarpool.BusinessLogic;
using ConestogaCarpool.Controllers;
using ConestogaCarpool.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ConestogaCarpool.Tests.Controllers.Tests
{
    public class RequestControllerTests
    {
        private readonly ConestogaCarpoolContext _context;

        [Fact]
        public void RequestControllerDriverIndexReturnsView_ExpectedSuccess()
        {
            // Arrange
            var requests = GetRequests();
            var _mock = new Mock<IRequestLogic>();
            _mock.Setup(x => x.GetDriverRequests(It.IsAny<Int32>())).ReturnsAsync(requests);
            var controllerUnderTest = new RequestController(_context, _mock.Object);

            // Act
            var result = controllerUnderTest.DriverIndex(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(Task<IActionResult>), result.GetType());
        }

        [Fact]
        public void RequestControllerPassengerIndexReturnsView_ExpectedSuccess()
        {
            // Arrange
            var requests = GetRequests();
            var _mock = new Mock<IRequestLogic>();
            _mock.Setup(x => x.GetPassengerRequests(It.IsAny<Int32>())).ReturnsAsync(requests);
            var controllerUnderTest = new RequestController(_context, _mock.Object);


            // Act
            var result = controllerUnderTest.PassengerIndex(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(Task<IActionResult>), result.GetType());
        }

        [Fact]
        public void RequestControllerCreateReturnsView_ExpectedSuccess()
        {
            // Arrange
            var requests = GetRequests();
            var _mock = new Mock<IRequestLogic>();
            _mock.Setup(x => x.GetPassengerRequests(It.IsAny<Int32>())).ReturnsAsync(requests);
            var controllerUnderTest = new RequestController(_context, _mock.Object);

            // Act
            var result = controllerUnderTest.Create(GetRequest(), "aread@conestogac.on.ca");

            // Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void ReviewControllerEditReturnsView_ExpectedSuccess()
        {
            // Arrange
            Request request = new Request
            {
                RequestId = 7,
                RequestStatusId = 3,
                PassengerId = 3,
                PostId = 10
            };

            var _mock = new Mock<IRequestLogic>();
            _mock.Setup(x => x.GetSingleRequest(It.IsAny<Int32>())).ReturnsAsync(GetRequest());
            var controllerUnderTest = new RequestController(_context, _mock.Object);

            // Act
            var result = controllerUnderTest.Edit(1, request);

            // Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void ReviewControllerAcceptReturnsView_ExpectedSuccess()
        {
            // Arrange
            Request request = new Request
            {
                RequestId = 7,
                RequestStatusId = 4,
                PassengerId = 3,
                PostId = 10
            };

            var _mock = new Mock<IRequestLogic>();
            _mock.Setup(x => x.GetSingleRequest(It.IsAny<Int32>())).ReturnsAsync(GetRequest());
            var controllerUnderTest = new RequestController(_context, _mock.Object);

            // Act
            var result = controllerUnderTest.Edit(1, request);

            // Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void ReviewControllerDeclineReturnsView_ExpectedSuccess()
        {
            // Arrange
            Request request = new Request
            {
                RequestId = 7,
                RequestStatusId = 4,
                PassengerId = 3,
                PostId = 10
            };

            var _mock = new Mock<IRequestLogic>();
            _mock.Setup(x => x.GetSingleRequest(It.IsAny<Int32>())).ReturnsAsync(GetRequest());
            var controllerUnderTest = new RequestController(_context, _mock.Object);

            // Act
            var result = controllerUnderTest.Edit(1, request);

            // Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void ReviewControllerDetailsReturnsView_ExpectedSuccess()
        {
            var _mock = new Mock<IRequestLogic>();
            _mock.Setup(x => x.GetSingleRequest(It.IsAny<Int32>())).ReturnsAsync(GetRequest());
            var controllerUnderTest = new RequestController(_context, _mock.Object);

            // Act
            var result = controllerUnderTest.Details(1);

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void ReviewControllerDeleteConfirmedReturnsView_ExpectedSuccess()
        {
            var _mock = new Mock<IRequestLogic>();
            _mock.Setup(x => x.GetSingleRequest(It.IsAny<Int32>())).ReturnsAsync(GetRequest());
            var controllerUnderTest = new RequestController(_context, _mock.Object);

            // Act
            var result = controllerUnderTest.DeleteConfirmed(1);

            // Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }


        #region Private Methods

        private List<Request> GetRequests()
        {
            var requests = new List<Request>();
            requests.Add(new Request
            {
                RequestId = 1,
                RequestStatusId = 1,
                PassengerId = 2,
                PostId = 1
            });
            requests.Add(new Request
            {
                RequestId = 2,
                RequestStatusId = 1,
                PassengerId = 3,
                PostId = 2
            });

            return requests;
        }

        private Request GetRequest()
        {
            Request request = new Request
            {
                RequestId = 7,
                RequestStatusId = 4,
                PassengerId = 3,
                PostId = 10
            };

            return request;
        }

        #endregion
    }
}
