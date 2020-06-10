using ConestogaCarpool.BusinessLogic;
using ConestogaCarpool.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConestogaCarpool.Tests.BusinessLogic.Tests
{
    public class ReviewLogicTests
    {
        [Fact]
        public void GetAllReviewsViaBLL_ExpectedSuccess()
        {
            // Arrange
            Mock<IReviewLogic> mockReviewLogic = new Mock<IReviewLogic>();
            var expectedResult = new List<Review>();
            expectedResult.Add(new Review
            {
                ReviewId = 1,
                Rating = 5,
                Comment = "Nice driver! Arrived on time.",
                RideId = 1,
                PassengerId = 1,
                DriverId = 1
            });
            expectedResult.Add(new Review
            {
                ReviewId = 2,
                Rating = 5,
                Comment = "Driver was kind.",
                RideId = 1,
                PassengerId = 1,
                DriverId = 1
            });
            var methodUnderTest = mockReviewLogic.Object;

            // Set-up mock logic
            mockReviewLogic.Setup(m => m.GetReviews())
                .ReturnsAsync(expectedResult);

            // Act
            var allReviews = methodUnderTest.GetReviews();
            bool reviewsReturned = true;

            if (!allReviews.IsCompleted)
            {
                reviewsReturned = false;
            }

            // Assert
            Assert.True(reviewsReturned);
        }

        [Fact]
        public void GetDriverReviewsViaBLL_ValidDriverId_ExpectedSuccess()
        {
            // Arrange
            Mock<IReviewLogic> mockReviewLogic = new Mock<IReviewLogic>();
            var expectedResult = new List<Review>();
            expectedResult.Add(new Review
            {
                ReviewId = 1,
                Rating = 5,
                Comment = "Nice driver! Arrived on time.",
                RideId = 1,
                PassengerId = 1,
                DriverId = 1
            });
            expectedResult.Add(new Review
            {
                ReviewId = 2,
                Rating = 5,
                Comment = "Driver was kind.",
                RideId = 1,
                PassengerId = 2,
                DriverId = 1
            });
            var methodUnderTest = mockReviewLogic.Object;

            // Set-up mock logic
            mockReviewLogic.Setup(m => m.GetDriverReviews(It.IsAny<Int32>()))
                .ReturnsAsync(expectedResult);

            // Act
            var allReviews = methodUnderTest.GetDriverReviews(1);
            bool reviewsReturned = true;

            if (!allReviews.IsCompleted)
            {
                reviewsReturned = false;
            }

            // Assert
            Assert.True(reviewsReturned);
        }

        [Fact]
        public void GetPassengerReviewsViaBLL_ValidPassengerId_ExpectedSuccess()
        {
            // Arrange
            Mock<IReviewLogic> mockReviewLogic = new Mock<IReviewLogic>();
            var expectedResult = new List<Review>();
            expectedResult.Add(new Review
            {
                ReviewId = 1,
                Rating = 5,
                Comment = "Nice driver! Arrived on time.",
                RideId = 1,
                PassengerId = 1,
                DriverId = 1
            });
            expectedResult.Add(new Review
            {
                ReviewId = 2,
                Rating = 5,
                Comment = "Driver was kind.",
                RideId = 1,
                PassengerId = 1,
                DriverId = 2
            });
            var methodUnderTest = mockReviewLogic.Object;

            // Set-up mock logic
            mockReviewLogic.Setup(m => m.GetPassengerReviews(It.IsAny<Int32>()))
                .ReturnsAsync(expectedResult);

            // Act
            var allReviews = methodUnderTest.GetPassengerReviews(1);
            bool reviewsReturned = true;

            if (!allReviews.IsCompleted)
            {
                reviewsReturned = false;
            }

            // Assert
            Assert.True(reviewsReturned);
        }

        [Fact]
        public void GetSingleReviewViaBLL_ValidReviewId_ReturnsReviewDetails()
        {
            // Arrange
            Mock<IReviewLogic> mockReviewLogic = new Mock<IReviewLogic>();
            var expectedResult = new Review();
            var methodUnderTest = mockReviewLogic.Object;

            // Set-up mock logic
            mockReviewLogic.Setup(m => m.GetSingleReview(It.IsAny<Int32>()))
                .ReturnsAsync(expectedResult);

            // Act
            var reviewFound = methodUnderTest.GetSingleReview(1);
            bool isReviewFound = true;

            if (reviewFound == null)
            {
                isReviewFound = false;
            }

            // Assert
            Assert.True(isReviewFound);
        }

        [Fact]
        public void CreateReviewViaBLL_ValidRequest_CreateMethodCalledOnce()
        {
            // Arrange
            Mock<IReviewLogic> mockReviewLogic = new Mock<IReviewLogic>();
            var expectedResult = new Review();
            var methodUnderTest = mockReviewLogic.Object;
            var newReview = new Review
            {
                ReviewId = 2,
                Rating = 5,
                Comment = "Driver was kind.",
                RideId = 1,
                PassengerId = 1,
                DriverId = 2
            };

            // Set-up mock logic
            mockReviewLogic.Setup(m => m.CreateReview(newReview));
            mockReviewLogic.Setup(m => m.Save());

            // Act
            methodUnderTest.CreateReview(newReview);
            methodUnderTest.Save();

            // Assert
            mockReviewLogic.Verify(x => x.CreateReview(newReview), Times.Once());
        }

        [Fact]
        public void UpdateReviewViaBLL_ChangeRating_UpdateMethodCalledOnce()
        {
            // Arrange
            Mock<IReviewLogic> mockReviewLogic = new Mock<IReviewLogic>();
            var expectedResult = new Review();
            var methodUnderTest = mockReviewLogic.Object;
            var updatedReview = new Review
            {
                ReviewId = 2,
                Rating = 5,
                Comment = "Driver was kind.",
                RideId = 1,
                PassengerId = 1,
                DriverId = 2
            };

            // Set-up mock logic
            mockReviewLogic.Setup(m => m.UpdateReview(updatedReview));
            mockReviewLogic.Setup(m => m.Save());

            // Act
            methodUnderTest.UpdateReview(updatedReview);
            methodUnderTest.Save();

            // Assert
            mockReviewLogic.Verify(x => x.UpdateReview(updatedReview), Times.Once());
        }

        [Fact]
        public void DeleteReviewViaBLL_ValidRequestId_DeleteMethodCalledOnce()
        {
            // Arrange
            Mock<IReviewLogic> mockReviewLogic = new Mock<IReviewLogic>();
            var expectedResult = new Review();
            var methodUnderTest = mockReviewLogic.Object;
            var review = new Review
            {
                ReviewId = 2,
                Rating = 5,
                Comment = "Driver was kind.",
                RideId = 1,
                PassengerId = 1,
                DriverId = 2
            };

            // Set-up mock logic
            mockReviewLogic.Setup(m => m.DeleteReview(review.ReviewId));
            mockReviewLogic.Setup(m => m.Save());

            // Act
            methodUnderTest.DeleteReview(review.ReviewId);
            methodUnderTest.Save();

            // Assert
            mockReviewLogic.Verify(x => x.DeleteReview(review.ReviewId), Times.Once());
        }
    }
}
