using Microsoft.EntityFrameworkCore;
using PerformanceSurvey.Context;
using PerformanceSurvey.iRepository;
using PerformanceSurvey.Models;
using PerformanceSurvey.Models.DTOs;
using System.Text;

using System.Security.Cryptography;

namespace PerformanceSurvey.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get user by email
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserEmail == email && !u.IsDisabled);
        }

        // Create a new user with hashed password
        public async Task<User> CreateUserAsync(User user)
        {
            // Hash the user's password before saving
            _context.users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // Get user by ID
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.users
                .Include(u => u.Department)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == id && !u.IsDisabled);
        }

        public async Task<List<User>> GetUsersByIdsAsync(List<int> userId)
        {
            return await _context.users
                .Where(u => userId.Contains(u.UserId))
                .ToListAsync();
        }

        // Update existing user details
        public async Task<User> UpdateUserAsync(int id, User user)
        {
            var existingUser = await _context.users.FindAsync(id);
            if (existingUser == null || existingUser.IsDisabled)
                return null;

            existingUser.Name = user.Name;
            existingUser.UserEmail = user.UserEmail;
            existingUser.UserType = user.UserType;
            existingUser.DepartmentId = user.DepartmentId;

            // Update password if provided
            if (!string.IsNullOrEmpty(user.Password))
            {
                existingUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            }

            _context.users.Update(existingUser);
            await _context.SaveChangesAsync();

            return existingUser;
        }

        // Retrieve all active users
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.users
                .Where(u => !u.IsDisabled)
                .AsNoTracking()
                .ToListAsync();
        }

        // Disable a user by setting IsDisabled to true
        public async Task<bool> DisableUserAsync(int id)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null || user.IsDisabled)
                return false;

            user.IsDisabled = true;
            _context.users.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }

        // Get all active users in a specific department
        public async Task<IEnumerable<User>> GetUsersByDepartmentIdAsync(int departmentId)
        {
            return await _context.users
                .Where(u => u.DepartmentId == departmentId && !u.IsDisabled)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByDepartmentIdsAsync(IEnumerable<int> departmentId)
        {
            if (departmentId == null || !departmentId.Any())
            {
                return Enumerable.Empty<User>(); // Return an empty collection if no department IDs are provided
            }

            return await _context.users
                .Where(u => departmentId.Contains(u.DepartmentId.Value))
                .ToListAsync();
        }



        //passwording

        public async Task UpdateUserPasswordAsync(User user)
        {
            // Implementation to update user password in the database
            // e.g., using Entity Framework Core
            _context.users.Update(user);
            await _context.SaveChangesAsync();
        }


        public async Task<User> GetAdminUserAsync()
        {
            return await _context.users
                .FirstOrDefaultAsync(u => u.UserType == UserType.AdminUser);
        }
    }
}


