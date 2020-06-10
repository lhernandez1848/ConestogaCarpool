using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaCarpool.Models
{
    public class DriverMetadata
    {
        public int DriverId { get; set; }
        public int UserId { get; set; }
        public int? LicenceClassId { get; set; }

        [Required(ErrorMessage = "Years of experience is required")]
        public int? Experience { get; set; }
    }

    [ModelMetadataType(typeof(DriverMetadata))]
    public partial class Driver : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Experience.HasValue)
            {
                var validExperience = DriverValidation.DriverExperienceValidation(Experience.Value);

                if (validExperience == false)
                {
                    yield return new ValidationResult("Invalid years of experience",
                        new[] { nameof(Experience) });
                }
            }

            yield return ValidationResult.Success;
        }
    }
}
