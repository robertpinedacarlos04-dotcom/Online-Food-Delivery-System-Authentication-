using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FoodSystem.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [MaxLength(500)]
        public string DeliveryAddress { get; set; }

        [MaxLength(100)]
        public string Country { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
