using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaCarpool.Models
{
    public class VehicleMetadata
    {
        public int VehicleId { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "Make is required")]
        [StringLength(50, ErrorMessage = "Make must be less than 50 characters")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Model is required")]
        [StringLength(50, ErrorMessage = "Model must be less than 50 characters")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Enter a valid model year")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Colour is required")]
        [StringLength(50, ErrorMessage = "Colour must be less than 50 characters")]
        public string Colour { get; set; }

        [StringLength(8, MinimumLength = 2, ErrorMessage = "Invalid license plate")]
        [RegularExpression(@"^[A-Z0-9]+$", ErrorMessage = "Invalid license plate")]
        public string Plate { get; set; }
    }

    [ModelMetadataType(typeof(VehicleMetadata))]
    public partial class Vehicle : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validYear = VehicleValidation.VehicleYearValidation(Year);

            if (validYear == false)
            {
                yield return new ValidationResult("Enter a valid model year",
                    new[] { nameof(Year) });
            }

            yield return ValidationResult.Success;
        }
    }
}
