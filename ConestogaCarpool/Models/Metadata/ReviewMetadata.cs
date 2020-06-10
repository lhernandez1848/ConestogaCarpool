using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaCarpool.Models
{
    public class ReviewMetadata
    {
        public int ReviewId { get; set; }

        [Required(ErrorMessage = "Rating is required.")]
        [Range(1, 5, ErrorMessage = "Rating can only be a whole number between 1-5.")]
        //[RegularExpression(@"^\d[1-5]$", ErrorMessage = "Enter a rating between 1 and 5.")]
        public int Rating { get; set; }

        [StringLength(255, ErrorMessage = "Comment must be less than 255 characters.")]
        public string Comment { get; set; }

        public int RideId { get; set; }
        public int PassengerId { get; set; }
        public int DriverId { get; set; }
    }
}
