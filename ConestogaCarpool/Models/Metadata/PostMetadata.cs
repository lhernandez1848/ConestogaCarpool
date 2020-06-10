using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaCarpool.Models
{
    public class PostMetadata
    {
        public int PostId { get; set; }
        public int PostStatusId { get; set; }
        public int DriverId { get; set; }
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "Destination is required")]
        public string Destination { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Time is required")]
        public TimeSpan Time { get; set; }
    }

    [ModelMetadataType(typeof(PostMetadata))]
    public partial class Post : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validDate = PostValidation.PostDateValidation(Date);

            if (validDate == false)
            {
                yield return new ValidationResult("Enter a valid date",
                    new[] { nameof(Date) });
            }

            yield return ValidationResult.Success;
        }
    }
}
