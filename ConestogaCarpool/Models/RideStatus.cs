using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConestogaCarpool.Models
{
    public partial class RideStatus
    {
        public RideStatus()
        {
            Ride = new HashSet<Ride>();
        }

        public int RideStatusId { get; set; }

        [Display(Name = "Ride Status")]
        public string RideStatusDescription { get; set; }

        public ICollection<Ride> Ride { get; set; }
    }
}
