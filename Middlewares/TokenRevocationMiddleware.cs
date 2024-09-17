using PerformanceSurvey.iRepository;
using PerformanceSurvey.Repository;

namespace PerformanceSurvey.Middlewares
{
    public class TokenRevocationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenRevocationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IAuthLoginRepository authLoginRepository)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null && await authLoginRepository.IsTokenRevokedAsync(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token has been revoked");
                return;
            }

            await _next(context);
        }
    }

}
