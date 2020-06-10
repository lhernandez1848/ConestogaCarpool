using ConestogaCarpool.BusinessLogic;
using ConestogaCarpool.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConestogaCarpool.Tests.BusinessLogic.Tests
{
    public class PostLogicTests
    {
        [Fact]
        public void GetAllPostsViaBLL_ExpectedSuccess()
        {
            // Arrange
            Mock<IPostLogic> mockPostLogic = new Mock<IPostLogic>();
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
                PostId = 1,
                PostStatusId = 1,
                DriverId = 1,
                VehicleId = 1,
                Destination = "Kitchener",
                Location = "Cambridge",
                Date = DateTime.Today,
                Time = new TimeSpan(11, 00, 00)
            });
            var methodUnderTest = mockPostLogic.Object;

            // Set-up mock logic
            mockPostLogic.Setup(m => m.GetAllPosts())
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
        public void GetSinglePostViaBLL_ValidPostId_ExpectedSuccess()
        {
            // Arrange
            Mock<IPostLogic> mockPostLogic = new Mock<IPostLogic>();
            var expectedResult = new Post();
            var methodUnderTest = mockPostLogic.Object;

            // Set-up mock logic
            mockPostLogic.Setup(m => m.GetSinglePost(It.IsAny<Int32>()))
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
        public void GetDriverPostsViaBLL_ValidDriverId_ExpectedSuccess()
        {
            // Arrange
            Mock<IPostLogic> mockPostLogic = new Mock<IPostLogic>();
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
            var methodUnderTest = mockPostLogic.Object;

            // Set-up mock logic
            mockPostLogic.Setup(m => m.GetDriverPosts(It.IsAny<Int32>()))
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
            Mock<IPostLogic> mockPostLogic = new Mock<IPostLogic>();
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
            var methodUnderTest = mockPostLogic.Object;

            // Set-up mock logic
            mockPostLogic.Setup(m => m.GetAvailableDrivers("Waterloo", "Kitchener"))
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
        public void CreatePostViaBLL_ValidPost_ExpectedCreateMethodCalledOnce()
        {
            // Arrange
            Mock<IPostLogic> mockPostLogic = new Mock<IPostLogic>();
            var methodUnderTest = mockPostLogic.Object;
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

            // Set-up mock logic
            mockPostLogic.Setup(m => m.CreatePost(newPost));
            mockPostLogic.Setup(m => m.Save());

            // Act
            methodUnderTest.CreatePost(newPost);
            methodUnderTest.Save();

            // Assert
            mockPostLogic.Verify(x => x.CreatePost(newPost), Times.Once());
        }

        [Fact]
        public void UpdatePostViaBLL_ChangeDestination_ExpectedUpdateMethodCalledOnce()
        {
            // Arrange
            Mock<IPostLogic> mockPostLogic = new Mock<IPostLogic>();
            var methodUnderTest = mockPostLogic.Object;

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

            // Set-up mock logic
            mockPostLogic.Setup(m => m.UpdatePost(updatedPost));
            mockPostLogic.Setup(m => m.Save());

            // Act
            methodUnderTest.UpdatePost(updatedPost);
            methodUnderTest.Save();

            // Assert
            mockPostLogic.Verify(x => x.UpdatePost(updatedPost), Times.Once());
        }

        [Fact]
        public void DeletePostViaBLL_ExpectedUpdateMethodCalledOnce()
        {
            // Arrange
            Mock<IPostLogic> mockPostLogic = new Mock<IPostLogic>();
            var methodUnderTest = mockPostLogic.Object;
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

            // Set-up mock logic
            mockPostLogic.Setup(m => m.DeletePost(post.PostId));
            mockPostLogic.Setup(m => m.Save());

            // Act
            methodUnderTest.DeletePost(post.PostId);
            methodUnderTest.Save();

            // Assert
            mockPostLogic.Verify(x => x.DeletePost(post.PostId), Times.Once());
        }

        /*
        [Fact]
        public void ValidPost_ShouldPassValidation()
        {
            bool recordAccepted = true;
            string validationResults = "";
            

            Post p = new Post();

            try
            {
                _context2.Post.Add(p);
                _context2.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                recordAccepted = false;
                validationResults = ex.GetBaseException().Message;
            }
            finally
            {
                try
                {
                    _context2.Entry(p).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                }
                catch (Exception ex)
                {
                    //
                }
            }

            Assert.False(recordAccepted, validationResults);
        }*/
    }
}
