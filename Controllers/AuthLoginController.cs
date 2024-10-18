﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerformanceSurvey.iServices;
using PerformanceSurvey.Models.DTOs;
using PerformanceSurvey.Models.RequestDTOs;

namespace PerformanceSurvey.Controllers
{
    [Route("api/PerformanceSurvey")]
    [ApiController]
    public class AuthLoginController : Controller
    {
        private readonly IAuthLoginService _authLoginService;
        private readonly ILogger<AuthLoginController> _logger;

        public AuthLoginController (IAuthLoginService authLoginService, ILogger<AuthLoginController> logger)
        {
            _authLoginService = authLoginService;
            _logger = logger;
        }

        [HttpPost("Adminlogin")]
        public async Task<IActionResult> Adminlogin([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var adminUser = await _authLoginService.AuthenticateAdminUserAsync(loginDto.UserEmail, loginDto.Password);

            if (adminUser == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(adminUser); // This can be modified to include a token if authentication is extended
        }



        [HttpPost("create-admin-user")]
        public async Task<IActionResult> CreateAdminUser([FromBody] CreateAdminUserRequest createAdminUserDto)
        {
            // Validate the input (ensure required fields are provided)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (createAdminUserDto.Key != "SuperAdminKey#")
            {
                return BadRequest("Invalid Key, Request key from super Admin");
            }
            // Call the service method to create the admin user
            var adminUser = await _authLoginService.CreateAdminUserAsync(createAdminUserDto);
            if (adminUser == null)
            {
                return BadRequest("Failed to create admin user.");
            }

            // Return the newly created admin user details
            return Ok(adminUser);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            // Step 1: Validate input
            if (string.IsNullOrEmpty(loginDto.UserEmail) || string.IsNullOrEmpty(loginDto.Password))
            {
                return BadRequest("Email and password are required.");
            }

            // Step 2: Authenticate the user
            var user = await _authLoginService.AuthenticateUserAsync(loginDto.UserEmail, loginDto.Password);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Step 3: Return success response (optionally generate and return a JWT token)
            return Ok(user);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromHeader(Name = "Authorization")] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is required");
            }

            token = token.Replace("Bearer ", "");
            await _authLoginService.RevokeTokenAsync(token); // Revoke the token

            return Ok("Token has been revoked");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("ChangeAdminPassword")]
        public async Task<IActionResult> ChangeAdminPassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            try
            {
                await _authLoginService.ChangeAdminPasswordAsync(changePasswordDto);
                return Ok(new { message = "Password changed successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while changing the password." });
            }
        }



    }
}
