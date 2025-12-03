// Controllers/AuthController.cs
using FoodSystem.DTOs;
using FoodSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);
            if (!result.Success)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            var token = await _authService.GeneratePasswordResetTokenAsync(forgotPasswordDto.Email);
            if (token == null)
            {
                return BadRequest(new { Message = "User not found" });
            }

            // In a real application, you would send this token via email
            return Ok(new { Message = "Password reset token generated", Token = token });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var result = await _authService.ResetPasswordAsync(resetPasswordDto);
            if (!result)
            {
                return BadRequest(new { Message = "Password reset failed" });
            }
            return Ok(new { Message = "Password reset successful" });
        }
    }
}