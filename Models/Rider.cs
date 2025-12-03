using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodSystem.Models
{
    public class Rider
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }


        public string Fullname { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }

        [MaxLength(100)]
        public string MotorcycleModel { get; set; }

        [Required]
        [MaxLength(20)]
        public string PlateNumber { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

}
