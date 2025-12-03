// DTOs/RegisterDto.cs
using System.ComponentModel.DataAnnotations;

namespace FoodSystem.DTOs
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(500)]
        public string DeliveryAddress { get; set; }

        [Required]
        [MaxLength(100)]
        public string Country { get; set; }
    }
}