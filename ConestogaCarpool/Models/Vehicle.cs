using System;
using System.Collections.Generic;

namespace ConestogaCarpool.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Post = new HashSet<Post>();
            VehicleImage = new HashSet<VehicleImage>();
        }

        public int VehicleId { get; set; }
        public int UserId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Colour { get; set; }
        public string Plate { get; set; }

        public User User { get; set; }
        public ICollection<Post> Post { get; set; }
        public ICollection<VehicleImage> VehicleImage { get; set; }
    }
}
