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
    public class ReviewControllerTests
    {
        private readonly ConestogaCarpoolContext _context;

        [Fact]
        public void ReviewControllerDriverIndexReturnsView_ExpectedSuccess()
        {
            // Arrange
            var reviews = GetReviews();
            var _mock = new Mock<IReviewLogic>();
            _mock.Setup(x => x.GetDriverReviews(It.IsAny<Int32>())).ReturnsAsync(reviews);
            var controllerUnderTest = new ReviewController(_context, _mock.Object);

            // Act
            var result = controllerUnderTest.DriverIndex(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(Task<IActionResult>), result.GetType());
        }

        [Fact]
        public void ReviewControllerPassengerIndexReturnsView_ExpectedSuccess()
        {
            // Arrange
            var reviews = GetReviews();
            var _mock = new Mock<IReviewLogic>();
            _mock.Setup(x => x.GetPassengerReviews(It.IsAny<Int32>())).ReturnsAsync(reviews);
            var controllerUnderTest = new ReviewController(_context, _mock.Object);

            // Act
            var result = controllerUnderTest.PassengerIndex(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeof(Task<IActionResult>), result.GetType());
        }

        [Fact]
        public void ReviewControllerCreateReturnsView_ExpectedSuccess()
        {
            // Arrange
            var reviews = GetReviews();
            var _mock = new Mock<IReviewLogic>();
            _mock.Setup(x => x.GetPassengerReviews(It.IsAny<Int32>())).ReturnsAsync(reviews);
            var controllerUnderTest = new ReviewController(_context, _mock.Object);

            // Act
            var result = controllerUnderTest.Create(GetReview());

            // Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void ReviewControllerEditReturnsView_ExpectedSuccess()
        {
            // Arrange
            Review review = new Review
            {
                ReviewId = 1,
                Rating = 5,
                Comment = "Nice driver! Arrived on time. Will book again next time!",
                RideId = 1,
                PassengerId = 1,
                DriverId = 1
            };

            var _mock = new Mock<IReviewLogic>();
            _mock.Setup(x => x.GetSingleReview(It.IsAny<Int32>())).ReturnsAsync(GetReview());
            var controllerUnderTest = new ReviewController(_context, _mock.Object);

            // Act
            var result = controllerUnderTest.Edit(1, review);

            // Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void ReviewControllerDetailsReturnsView_ExpectedSuccess()
        {
            // Arrange
            var _mock = new Mock<IReviewLogic>();
            _mock.Setup(x => x.GetSingleReview(It.IsAny<Int32>())).ReturnsAsync(GetReview());
            var controllerUnderTest = new ReviewController(_context, _mock.Object);

            // Act
            var result = controllerUnderTest.Details(1);

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void ReviewControllerDeleteConfirmedReturnsView_ExpectedSuccess()
        {
            // Arrange
            var _mock = new Mock<IReviewLogic>();
            _mock.Setup(x => x.GetSingleReview(It.IsAny<Int32>())).ReturnsAsync(GetReview());
            var controllerUnderTest = new ReviewController(_context, _mock.Object);

            // Act
            var result = controllerUnderTest.DeleteConfirmed(1);

            // Assert
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        #region Private Methods

        private List<Review> GetReviews()
        {
            var reviews = new List<Review>();
            reviews.Add(new Review
            {
                ReviewId = 1,
                Rating = 5,
                Comment = "Nice driver! Arrived on time.",
                RideId = 1,
                PassengerId = 1,
                DriverId = 1
            });
            reviews.Add(new Review
            {
                ReviewId = 2,
                Rating = 5,
                Comment = "Driver was kind.",
                RideId = 1,
                PassengerId = 2,
                DriverId = 1
            });

            return reviews;
        }

        private Review GetReview()
        {
            Review review = new Review
            {
                ReviewId = 1,
                Rating = 5,
                Comment = "Nice driver! Arrived on time.",
                RideId = 1,
                PassengerId = 1,
                DriverId = 1
            };

            return review;
        }
        #endregion
    }
}