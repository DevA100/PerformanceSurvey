using PerformanceSurvey.Models.DTOs.ResponseDTOs;
using PerformanceSurvey.Models.DTOs;
using PerformanceSurvey.Models;

namespace PerformanceSurvey.iServices
{
    public interface IAuthLoginService
    {
        Task<AuthenticateAdminDto> AuthenticateUserAsync(string email, string password);
        Task<CreateAdminUserResponse> CreateAdminUserAsync(CreateAdminUserRequest adminUserDto);
        Task<AuthenticateAdminDto> AuthenticateAdminUserAsync(string email, string password);

        Task RevokeTokenAsync(string token);

    }
}
