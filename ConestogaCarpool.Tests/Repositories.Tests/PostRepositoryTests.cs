using ConestogaCarpool.Models;
using ConestogaCarpool.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConestogaCarpool.Tests.Repositories.Tests
{
    public class PostRepositoryTests
    {
        [Fact]
        public void GetAllPostsViaRepository_ExpectedSuccess()
        {
            // Arrange
            Mock<IPostRepository> mockPostRepository = new Mock<IPostRepository>();
            var expectedResult = new List<Post>();
            expectedResult.Add(new Post
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
            expectedResult.Add(new Post
            {
                PostId = 2,
                PostStatusId = 1,
                DriverId = 1,
                VehicleId = 1,
                Destination = "Kitchener",
                Location = "Cambridge",
                Date = DateTime.Today,
                Time = new TimeSpan(11, 00, 00)
            });
            var methodUnderTest = mockPostRepository.Object;

            // Set-up mock repository
            mockPostRepository.Setup(m => m.GetAllPosts())
                .ReturnsAsync(expectedResult);

            // Act
            var allPosts = methodUnderTest.GetAllPosts();
            bool postsReturned = true;

            if (!allPosts.IsCompleted)
            {
                postsReturned = false;
            }

            // Assert
            Assert.True(postsReturned);
        }

        [Fact]
        public void GetSinglePostViaRepository_ValidPostId_ExpectedSuccess()
        {
            // Arrange
            Mock<IPostRepository> mockPostRepository = new Mock<IPostRepository>();
            var expectedResult = new Post();
            var methodUnderTest = mockPostRepository.Object;

            // Set-up mock repository
            mockPostRepository.Setup(m => m.GetSinglePost(It.IsAny<Int32>()))
                .ReturnsAsync(expectedResult);

            // Act
            var postFound = methodUnderTest.GetSinglePost(1);
            bool isPostFound = true;

            if (postFound == null)
            {
                isPostFound = false;
            }

            // Assert
            Assert.True(isPostFound);
        }

        [Fact]
        public void GetDriverPostsViaRepository_ValidDriverId_ExpectedSuccess()
        {
            // Arrange
            Mock<IPostRepository> mockPostRepository = new Mock<IPostRepository>();
            var expectedResult = new List<Post>();
            expectedResult.Add(new Post
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
            expectedResult.Add(new Post
            {
                PostId = 2,
                PostStatusId = 2,
                DriverId = 1,
                VehicleId = 1,
                Destination = "Kitchener",
                Location = "Cambridge",
                Date = DateTime.Today,
                Time = new TimeSpan(11, 00, 00)
            });
            var methodUnderTest = mockPostRepository.Object;

            // Set-up mock repository
            mockPostRepository.Setup(m => m.GetDriverPosts(It.IsAny<Int32>()))
                .ReturnsAsync(expectedResult);

            // Act
            var allPosts = methodUnderTest.GetDriverPosts(1);
            bool postsReturned = true;

            if (!allPosts.IsCompleted)
            {
                postsReturned = false;
            }

            // Assert
            Assert.True(postsReturned);
        }

        [Fact]
        public void GetAvailableDrivers_ValidLocationAndDestination_ExpectedSuccess()
        {
            // Arrange
            Mock<IPostRepository> mockPostRepository = new Mock<IPostRepository>();
            var expectedResult = new List<Post>();
            expectedResult.Add(new Post
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
            expectedResult.Add(new Post
            {
                PostId = 2,
                PostStatusId = 1,
                DriverId = 2,
                VehicleId = 2,
                Destination = "Kitchener",
                Location = "Waterloo",
                Date = DateTime.Today,
                Time = new TimeSpan(11, 00, 00)
            });
            var methodUnderTest = mockPostRepository.Object;

            // Set-up mock repository
            mockPostRepository.Setup(m => m.GetAvailableDrivers("Waterloo", "Kitchener"))
                .ReturnsAsync(expectedResult);

            // Act
            var allPosts = methodUnderTest.GetAvailableDrivers("Waterloo", "Kitchener");
            bool postsReturned = true;

            if (!allPosts.IsCompleted)
            {
                postsReturned = false;
            }

            // Assert
            Assert.True(postsReturned);
        }

        [Fact]
        public void CreatePostViaRepository_ValidPost_ExpectedCreateMethodCalledOnce()
        {
            // Arrange
            Mock<IPostRepository> mockPostRepository = new Mock<IPostRepository>();
            var methodUnderTest = mockPostRepository.Object;
            var newPost = new Post
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

            // Set-up mock repository
            mockPostRepository.Setup(m => m.CreatePost(newPost));
            mockPostRepository.Setup(m => m.Save());

            // Act
            methodUnderTest.CreatePost(newPost);
            methodUnderTest.Save();

            // Assert
            mockPostRepository.Verify(x => x.CreatePost(newPost), Times.Once());
        }

        [Fact]
        public void UpdatePostViaRepository_ChangeDestination_ExpectedUpdateMethodCalledOnce()
        {
            // Arrange
            Mock<IPostRepository> mockPostRepository = new Mock<IPostRepository>();
            var methodUnderTest = mockPostRepository.Object;

            var updatedPost = new Post
            {
                PostId = 1,
                PostStatusId = 1,
                DriverId = 1,
                VehicleId = 1,
                Destination = "Guelph",
                Location = "Waterloo",
                Date = DateTime.Today,
                Time = new TimeSpan(12, 00, 00)
            };

            // Set-up mock repository
            mockPostRepository.Setup(m => m.UpdatePost(updatedPost));
            mockPostRepository.Setup(m => m.Save());

            // Act
            methodUnderTest.UpdatePost(updatedPost);
            methodUnderTest.Save();

            // Assert
            mockPostRepository.Verify(x => x.UpdatePost(updatedPost), Times.Once());
        }

        [Fact]
        public void DeletePostViaRepository_ExpectedUpdateMethodCalledOnce()
        {
            // Arrange
            Mock<IPostRepository> mockPostRepository = new Mock<IPostRepository>();
            var methodUnderTest = mockPostRepository.Object;
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

            // Set-up mock repository
            mockPostRepository.Setup(m => m.DeletePost(post.PostId));
            mockPostRepository.Setup(m => m.Save());

            // Act
            methodUnderTest.DeletePost(post.PostId);
            methodUnderTest.Save();

            // Assert
            mockPostRepository.Verify(x => x.DeletePost(post.PostId), Times.Once());
        }
    }
}
