using Microsoft.AspNetCore.Mvc;
using Online_Food_Delivery_System_Authentication.DTOs;
using Online_Food_Delivery_System_Authentication.Services;

namespace Online_Food_Delivery_System_Authentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecondaryAdminController : ControllerBase
    {
        private readonly SecondaryAdminService _secondaryAdminService;

        public SecondaryAdminController()
        {
            _secondaryAdminService = new SecondaryAdminService();
        }

        /// <summary>
        /// Creates a new Secondary Admin account
        /// </summary>
        /// <param name="dto">Secondary Admin details</param>
        /// <returns>Created Secondary Admin</returns>
        [HttpPost("create")]
        public IActionResult CreateSecondaryAdmin([FromBody] CreateSecondaryAdminDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the current admin ID from authentication/session
            // For now, using a placeholder value
            int currentAdminId = 1; // Replace with actual logged-in admin ID

            var result = _secondaryAdminService.CreateSecondaryAdmin(dto, currentAdminId);

            if (result.IsSuccess)
            {
                return Ok(new
                {
                    success = true,
                    message = result.Message,
                    data = new
                    {
                        id = result.Data.Id,
                        fullName = result.Data.FullName,
                        email = result.Data.Email,
                        phoneNumber = result.Data.PhoneNumber,
                        createdAt = result.Data.CreatedAt,
                        isActive = result.Data.IsActive
                    }
                });
            }

            return BadRequest(new
            {
                success = false,
                message = result.Message
            });
        }
    }
}