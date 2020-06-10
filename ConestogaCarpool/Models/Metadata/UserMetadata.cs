using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaCarpool.Models
{
    public class UserMetadata
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Username must be between 6 and 15 characters")]
        public string Username { get; set; }

        public string Password { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name must be less than 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name must be less than 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(100, ErrorMessage = "Email must be less than 100 characters")]
        public string Email { get; set; }

        public string VerifiedEmail { get; set; }
    }

    [ModelMetadataType(typeof(UserMetadata))]
    public partial class User : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!String.IsNullOrWhiteSpace(Email))
            {
                var validEmail = UserValidation.UserEmailValidation(Email);

                if (validEmail == false)
                {
                    yield return new ValidationResult("Enter your Conestoga email address",
                        new[] { nameof(Email) });
                }
            }

            yield return ValidationResult.Success;
        }
    }
}
