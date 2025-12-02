using System.ComponentModel.DataAnnotations;

namespace Online_Food_Delivery_System_Authentication.DTOs
{
    public class CreateSecondaryAdminDto
    {
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100, MinimumLength = 2)]
        public string FullName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Phone Number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string PhoneNumber { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; } = default!;

        public static Result<T> Success(T data, string message = "Success")
        {
            return new Result<T> { IsSuccess = true, Data = data, Message = message };
        }

        public static Result<T> Failure(string message)
        {
            return new Result<T> { IsSuccess = false, Message = message };
        }
    }
}