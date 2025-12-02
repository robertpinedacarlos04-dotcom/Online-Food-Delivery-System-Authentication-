using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Online_Food_Delivery_System_Authentication.Models;
using Online_Food_Delivery_System_Authentication.DTOs;

namespace Online_Food_Delivery_System_Authentication.Services
{
    public class SecondaryAdminService
    {
        // TODO: Replace with actual database context
        private static List<SecondaryAdmin> _adminDatabase = new List<SecondaryAdmin>();
        private static int _nextId = 1;

        public Result<SecondaryAdmin> CreateSecondaryAdmin(CreateSecondaryAdminDto dto, int createdByAdminId)
        {
            try
            {
                // Validate password strength
                if (!IsStrongPassword(dto.Password))
                {
                    return Result<SecondaryAdmin>.Failure(
                        "Password must contain at least one uppercase, one lowercase, one number, and one special character");
                }

                // Check if email already exists
                if (EmailExists(dto.Email))
                {
                    return Result<SecondaryAdmin>.Failure("Email already exists in the system");
                }

                // Check if phone number already exists
                if (PhoneNumberExists(dto.PhoneNumber))
                {
                    return Result<SecondaryAdmin>.Failure("Phone number already exists in the system");
                }

                // Create new admin
                var admin = new SecondaryAdmin
                {
                    Id = _nextId++,
                    FullName = dto.FullName,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    Password = HashPassword(dto.Password), // Hash the password
                    CreatedAt = DateTime.UtcNow,
                    CreatedByAdminId = createdByAdminId,
                    IsActive = true
                };

                // Save to database (simulated)
                _adminDatabase.Add(admin);

                return Result<SecondaryAdmin>.Success(admin, "Secondary Admin created successfully");
            }
            catch (Exception ex)
            {
                return Result<SecondaryAdmin>.Failure($"Error creating Secondary Admin: {ex.Message}");
            }
        }

        private bool IsStrongPassword(string password)
        {
            var hasUpperCase = Regex.IsMatch(password, @"[A-Z]");
            var hasLowerCase = Regex.IsMatch(password, @"[a-z]");
            var hasNumber = Regex.IsMatch(password, @"\d");
            var hasSpecialChar = Regex.IsMatch(password, @"[!@#$%^&*(),.?""':{}|<>]");
            
            return hasUpperCase && hasLowerCase && hasNumber && hasSpecialChar;
        }

        private bool EmailExists(string email)
        {
            return _adminDatabase.Any(a => a.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        private bool PhoneNumberExists(string phoneNumber)
        {
            return _adminDatabase.Any(a => a.PhoneNumber == phoneNumber);
        }

        private string HashPassword(string password)
        {
            // TODO: In production, use BCrypt.Net-Next library
            // Install: Install-Package BCrypt.Net-Next
            // return BCrypt.Net.BCrypt.HashPassword(password);
            
            // Temporary implementation with SHA256
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}