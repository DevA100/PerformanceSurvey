using PerformanceSurvey.Models;

namespace PerformanceSurvey.iRepository
{
    public interface IAuthLoginRepository
    {
        Task<User> GetUserByEmailAndPasswordAsync(string email, string password);
        Task CreateAdminUserAsync(User adminUser);


        Task<bool> IsTokenRevokedAsync(string token);
        Task RevokeTokenAsync(string token);
    }
}
