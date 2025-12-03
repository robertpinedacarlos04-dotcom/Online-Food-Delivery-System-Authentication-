// DTOs/UserResponseDto.cs
namespace FoodSystem.DTOs
{
    public class UserResponseDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DeliveryAddress { get; set; }
        public string Country { get; set; }
        public string Role { get; set; }
    }
}