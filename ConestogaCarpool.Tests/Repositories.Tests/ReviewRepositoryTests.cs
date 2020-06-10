using ConestogaCarpool.Models;
using ConestogaCarpool.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConestogaCarpool.Tests.Repositories.Tests
{
    public class ReviewRepositoryTests
    {
        [Fact]
        public void GetAllReviewsViaRepository_ExpectedSuccess()
        {
            // Arrange
            Mock<IReviewRepository> mockReviewRepository = new Mock<IReviewRepository>();
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
            var methodUnderTest = mockReviewRepository.Object;

            // Set-up mock repository
            mockReviewRepository.Setup(m => m.GetReviews())
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
        public void GetDriverReviewsViaRepository_ValidDriverId_ExpectedSuccess()
        {
            // Arrange
            Mock<IReviewRepository> mockReviewRepository = new Mock<IReviewRepository>();
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
            var methodUnderTest = mockReviewRepository.Object;

            // Set-up mock repository
            mockReviewRepository.Setup(m => m.GetDriverReviews(It.IsAny<Int32>()))
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
        public void GetPassengerReviewsViaRepository_ValidPassengerId_ExpectedSuccess()
        {
            // Arrange
            Mock<IReviewRepository> mockReviewRepository = new Mock<IReviewRepository>();
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
            var methodUnderTest = mockReviewRepository.Object;

            // Set-up mock repository
            mockReviewRepository.Setup(m => m.GetPassengerReviews(It.IsAny<Int32>()))
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
        public void GetSingleReviewViaRepository_ValidReviewId_ReturnsReviewDetails()
        {
            // Arrange
            Mock<IReviewRepository> mockReviewRepository = new Mock<IReviewRepository>();
            var expectedResult = new Review();
            var methodUnderTest = mockReviewRepository.Object;

            // Set-up mock repository
            mockReviewRepository.Setup(m => m.GetSingleReview(It.IsAny<Int32>()))
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
        public void CreateReviewViaRepository_ValidRequest_CreateMethodCalledOnce()
        {
            // Arrange
            Mock<IReviewRepository> mockReviewRepository = new Mock<IReviewRepository>();
            var expectedResult = new Review();
            var methodUnderTest = mockReviewRepository.Object;
            var newReview = new Review
            {
                ReviewId = 2,
                Rating = 5,
                Comment = "Driver was kind.",
                RideId = 1,
                PassengerId = 1,
                DriverId = 2
            };

            // Set-up mock repository
            mockReviewRepository.Setup(m => m.CreateReview(newReview));
            mockReviewRepository.Setup(m => m.Save());

            // Act
            methodUnderTest.CreateReview(newReview);
            methodUnderTest.Save();

            // Assert
            mockReviewRepository.Verify(x => x.CreateReview(newReview), Times.Once());
        }

        [Fact]
        public void UpdateReviewViaRepository_ChangeRating_UpdateMethodCalledOnce()
        {
            // Arrange
            Mock<IReviewRepository> mockReviewRepository = new Mock<IReviewRepository>();
            var expectedResult = new Review();
            var methodUnderTest = mockReviewRepository.Object;
            var updatedReview = new Review
            {
                ReviewId = 2,
                Rating = 5,
                Comment = "Driver was kind.",
                RideId = 1,
                PassengerId = 1,
                DriverId = 2
            };

            // Set-up mock repository
            mockReviewRepository.Setup(m => m.UpdateReview(updatedReview));
            mockReviewRepository.Setup(m => m.Save());

            // Act
            methodUnderTest.UpdateReview(updatedReview);
            methodUnderTest.Save();

            // Assert
            mockReviewRepository.Verify(x => x.UpdateReview(updatedReview), Times.Once());
        }

        [Fact]
        public void DeleteReviewViaRepository_ValidRequestId_DeleteMethodCalledOnce()
        {
            // Arrange
            Mock<IReviewRepository> mockReviewRepository = new Mock<IReviewRepository>();
            var expectedResult = new Review();
            var methodUnderTest = mockReviewRepository.Object;
            var review = new Review
            {
                ReviewId = 2,
                Rating = 5,
                Comment = "Driver was kind.",
                RideId = 1,
                PassengerId = 1,
                DriverId = 2
            };

            // Set-up mock repository
            mockReviewRepository.Setup(m => m.DeleteReview(review.ReviewId));
            mockReviewRepository.Setup(m => m.Save());

            // Act
            methodUnderTest.DeleteReview(review.ReviewId);
            methodUnderTest.Save();

            // Assert
            mockReviewRepository.Verify(x => x.DeleteReview(review.ReviewId), Times.Once());
        }
    }
}
