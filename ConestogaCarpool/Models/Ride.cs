using System;
using System.Collections.Generic;

namespace ConestogaCarpool.Models
{
    public partial class Ride
    {
        public Ride()
        {
            Review = new HashSet<Review>();
        }

        public int RideId { get; set; }
        public int RideStatusId { get; set; }
        public int PostId { get; set; }
        public int RequestId { get; set; }

        public Post Post { get; set; }
        public Request Request { get; set; }
        public RideStatus RideStatus { get; set; }
        public ICollection<Review> Review { get; set; }
    }
}
