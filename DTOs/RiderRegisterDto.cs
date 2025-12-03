using System.ComponentModel.DataAnnotations;

namespace FoodSystem.DTOs
{
    public class RiderRegisterDto
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        public string Country { get; set; }

        [Required]
        [MaxLength(100)]
        public string MotorcycleModel { get; set; }

        [Required]
        [MaxLength(20)]
        public string PlateNumber { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
