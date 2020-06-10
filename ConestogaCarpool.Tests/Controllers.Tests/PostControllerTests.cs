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
    public class PostControllerTests
    {
        private readonly ConestogaCarpoolContext _context;

        [Fact]
        public void PostControllerIndexReturnsView_ExpectedSuccess()
        {
            // Arrange
            var posts = GetPosts();
            var _mock = new Mock<IPostLogic>();
            _mock.Setup(x => x.GetAllPosts()).ReturnsAsync(posts);
            var controllerUnderTest = new PostController(_context, _mock.Object);

            // Act
            var result = controllerUnderTest.DriverIndex(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(Task<IActionResult>), result.GetType());
        }

        [Fact]
        public void PostControllerPassengerIndexReturnsView_ExpectedSuccess()
        {
            // Arrange
            var posts = GetPosts();
            var _mock = new Mock<IPostLogic>();
            _mock.Setup(x => x.GetAllPosts()).ReturnsAsync(posts);
            var controllerUnderTest = new PostController(_context, _mock.Object);

            // Act
            var result = controllerUnderTest.PassengerIndex("Kitchener", "Waterloo");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(Task<IActionResult>), result.GetType());
        }

        [Fact]
        public void PostControllerCreate_InvalidPost_ReturnCreateViewWithPost()
        {
            // Arrange
            Mock<IPostLogic> mockPostLogic = new Mock<IPostLogic>();
            mockPostLogic.Setup(x => x.GetAllPosts()).ReturnsAsync(GetPosts());
            var controllerUnderTest = new PostController(_context, mockPostLogic.Object);

            var model = new Post();
            controllerUnderTest.ModelState.AddModelError("error", "Invalid post.");

            // Act
            var result = controllerUnderTest.Create(model);

            // Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void PostControllerEditReturnsView_ExpectedSuccess()
        {
            // Arrange
            Post post = new Post
            {
                PostStatusId = 1,
                DriverId = 1,
                VehicleId = 1,
                Destination = "Kitchener",
                Location = "Cambridge",
                Date = DateTime.Today,
                Time = new TimeSpan(11, 00, 00)
            };

            Mock<IPostLogic> mockPostLogic = new Mock<IPostLogic>();
            mockPostLogic.Setup(x => x.GetSinglePost(It.IsAny<Int32>())).ReturnsAsync(GetPost());
            var controllerUnderTest = new PostController(_context, mockPostLogic.Object);

            // Act
            var result = controllerUnderTest.Edit(1, post);

            // Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void PostControllerDetailsReturnsView_ExpectedSuccess()
        {
            // Arrange
            Mock<IPostLogic> mockPostLogic = new Mock<IPostLogic>();
            mockPostLogic.Setup(x => x.GetSinglePost(It.IsAny<Int32>())).ReturnsAsync(GetPost());
            var controllerUnderTest = new PostController(_context, mockPostLogic.Object);

            // Act
            var result = controllerUnderTest.Details(1);

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void PostControllerDeleteConfirmedReturnsView_ExpectedSuccess()
        {
            // Arrange
            Mock<IPostLogic> mockPostLogic = new Mock<IPostLogic>();
            mockPostLogic.Setup(x => x.GetSinglePost(It.IsAny<Int32>())).ReturnsAsync(GetPost());
            var controllerUnderTest = new PostController(_context, mockPostLogic.Object);

            // Act
            var result = controllerUnderTest.DeleteConfirmed(1);

            // Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void PostControllerSendRequestReturnsView_ExpectedSuccess()
        {
            // Arrange
            var posts = GetPosts();
            var _mock = new Mock<IPostLogic>();
            _mock.Setup(x => x.GetAllPosts()).ReturnsAsync(posts);
            var controllerUnderTest = new PostController(_context, _mock.Object);

            // Act
            var result = controllerUnderTest.SendRequest(1);

            // Assert
            Assert.Equal(typeof(Task<IActionResult>), result.GetType());
        }


        #region Private Methods
        private List<Post> GetPosts()
        {
            var posts = new List<Post>();
            posts.Add(new Post
            {
                PostId = 1,
                PostStatusId = 1,
                DriverId = 1,
                VehicleId = 1,
                Destination = "Kitchener",
                Location = "Waterloo",
                Date = DateTime.Today,
                Time = new TimeSpan(12, 00, 00)
            });
            posts.Add(new Post
            {
                PostId = 1,
                PostStatusId = 1,
                DriverId = 1,
                VehicleId = 1,
                Destination = "Kitchener",
                Location = "Cambridge",
                Date = DateTime.Today,
                Time = new TimeSpan(11, 00, 00)
            });

            return posts;
        }

        private Post GetPost()
        {
            var post = new Post
            {
                PostId = 1,
                PostStatusId = 1,
                DriverId = 1,
                VehicleId = 1,
                Destination = "Kitchener",
                Location = "Waterloo",
                Date = DateTime.Today,
                Time = new TimeSpan(12, 00, 00)
            };

            return post;
        }
        #endregion
    }
}
