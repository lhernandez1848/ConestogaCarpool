using ConestogaCarpool.Controllers;
using ConestogaCarpool.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConestogaCarpool.Tests.Controllers.Tests
{
    public class UserControllerTests
    {
        #region Private Methods

        private User GetUser()
        {
            User user = new User
            {
                UserId = 1,
                Username = "aread",
                Password = "aread123",
                FirstName = "Arthur",
                LastName = "Read",
                Email = "aread@conestogac.on.ca",
                VerifiedEmail = null
            };

            return user;
        }
        #endregion
    }
}
