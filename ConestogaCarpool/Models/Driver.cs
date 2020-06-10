using System;
using System.Collections.Generic;

namespace ConestogaCarpool.Models
{
    public partial class Driver
    {
        public Driver()
        {
            Post = new HashSet<Post>();
            Review = new HashSet<Review>();
        }

        public int DriverId { get; set; }
        public int UserId { get; set; }
        public int? LicenceClassId { get; set; }
        public int? Experience { get; set; }

        public LicenceClass LicenceClass { get; set; }
        public User User { get; set; }
        public ICollection<Post> Post { get; set; }
        public ICollection<Review> Review { get; set; }
    }
}
