using PerformanceSurvey.iRepository;
using PerformanceSurvey.iServices;
using PerformanceSurvey.Models;
using PerformanceSurvey.Models.DTOs;
using PerformanceSurvey.Repository;
using System.Text;

using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using PerformanceSurvey.Utilities;
using PerformanceSurvey.Models.DTOs.ResponseDTOs;

namespace PerformanceSurvey.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository,JwtTokenUtil jwtTokenUtil )
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> CreateUserAsync(UserRequest userDto)
        {
            // Validate email format
            if (!IsValidEmail(userDto.UserEmail))
            {
                throw new ArgumentException("Invalid email format.");
            }

            // Check if the email already exists
            var existingUser = await _userRepository.GetUserByEmailAsync(userDto.UserEmail);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Email is already in use.");
            }

            // Hash the password before creating the user

            var user = new User
            {
                Name = userDto.Name,
                UserEmail = userDto.UserEmail,
                DepartmentId = userDto.DepartmentId,
            };

            var createdUser = await _userRepository.CreateUserAsync(user);

            return new UserResponse
            {
                Name = createdUser.Name,
                UserEmail = createdUser.UserEmail,
            };
        }

        public async Task<UserResponse> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null || user.IsDisabled) return null; // Ensure the user is active

            return new UserResponse
            {
                Name = user.Name,
                UserEmail = user.UserEmail,
                // Password is intentionally omitted to avoid exposing it
            };
        }

        public async Task<List<UserResponse>> GetUsersByIdsAsync(List<int> userIds)
        {
            // Fetch users from the repository using the list of user IDs
            var users = await _userRepository.GetUsersByIdsAsync(userIds);

            // Map the users to the DTOs
            var userDtos = users.Select(user => new UserResponse
            {
                Name = user.Name,
                UserEmail = user.UserEmail,
                // Add any additional mappings here
            }).ToList();

            return userDtos;
        }


        public async Task<UserResponse> UpdateUserAsync(int id, UserRequest userDto)
        {
            var user = new User
            {
                UserId = id, // Ensure we use the correct UserId
                Name = userDto.Name,
                UserEmail = userDto.UserEmail,
                DepartmentId = userDto.DepartmentId,
                // Update password only if it's provided
            };

            var updatedUser = await _userRepository.UpdateUserAsync(id, user);
            if (updatedUser == null) return null;

            return new UserResponse
            {
                Name = updatedUser.Name,
                UserEmail = updatedUser.UserEmail,
            };
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync(); // Only active users are returned
            return users.Select(user => new UserResponse
            {
                UserId =user.UserId,
                Name = user.Name,
                UserEmail = user.UserEmail,
            });
        }

        public async Task<bool> DisableUserAsync(int id)
        {
            return await _userRepository.DisableUserAsync(id);
        }

        public async Task<IEnumerable<UserResponse>> GetUsersByDepartmentIdAsync(int departmentId)
        {
            var users = await _userRepository.GetUsersByDepartmentIdAsync(departmentId);
            return users.Select(user => new UserResponse
            {
                UserId = user.UserId,
                Name = user.Name,
                UserEmail = user.UserEmail,
            });
        }
        public async Task<IEnumerable<UserResponse>> GetUsersByDepartmentIdsAsync(IEnumerable<int> departmentId)
        {
            if (departmentId == null || !departmentId.Any())
            {
                return Enumerable.Empty<UserResponse>();
            }

            var users = await _userRepository.GetUsersByDepartmentIdsAsync(departmentId);

            // Map to UserDto
            var userDtos = users.Select(u => new UserResponse
            {
                Name = u.Name,
                UserEmail = u.UserEmail,
            }).ToList();

            return userDtos;
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        public async Task<string> GetAdminEmailAsync()
        {
            // Fetch the admin user from the repository
            var adminUser = await _userRepository.GetAdminUserAsync();
            return adminUser?.UserEmail;
        }



        // Example of a simple HashPassword function





    }

}
