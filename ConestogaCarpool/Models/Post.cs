using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConestogaCarpool.Models
{
    public partial class Post
    {
        public Post()
        {
            Request = new HashSet<Request>();
            Ride = new HashSet<Ride>();
        }

        public int PostId { get; set; }

        [Display(Name = "Post Status")]
        public int PostStatusId { get; set; }
        public int DriverId { get; set; }
        public int VehicleId { get; set; }
        public string Destination { get; set; }
        public string Location { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        public Driver Driver { get; set; }
        public PostStatus PostStatus { get; set; }
        public Vehicle Vehicle { get; set; }
        public ICollection<Request> Request { get; set; }
        public ICollection<Ride> Ride { get; set; }
    }
}
