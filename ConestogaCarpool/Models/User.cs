using System;
using System.Collections.Generic;

namespace ConestogaCarpool.Models
{
    public partial class User
    {
        public User()
        {
            Driver = new HashSet<Driver>();
            Request = new HashSet<Request>();
            Review = new HashSet<Review>();
            UserImage = new HashSet<UserImage>();
            Vehicle = new HashSet<Vehicle>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string VerifiedEmail { get; set; }

        public ICollection<Driver> Driver { get; set; }
        public ICollection<Request> Request { get; set; }
        public ICollection<Review> Review { get; set; }
        public ICollection<UserImage> UserImage { get; set; }
        public ICollection<Vehicle> Vehicle { get; set; }
    }
}
