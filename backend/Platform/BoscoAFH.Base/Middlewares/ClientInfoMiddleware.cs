using BoscoAFH.Common;
using BoscoAFH.CommonService;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using UAParser;

namespace BoscoAFH.Base.Middlewares
{
    public class ClientInfoMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context, IServiceProvider serviceProvider)
        {
            var user = context.User;

            // Skip processing if the user is not authenticated (no token)
            if (user.Identity?.IsAuthenticated != true)
            {
                await _next(context);
                return;
            }

            var userAgent = context.Request.Headers["User-Agent"].ToString();
            var correlationId = context.Request.Headers["X-Request-Id"].ToString();
            var parser = Parser.GetDefault();
            var clientInfo = parser.Parse(userAgent);

            // Resolve the scoped service within the request scope
            var currentUserService = serviceProvider.GetRequiredService<ICurrentUserService>();
            var allClaims = user.Claims.ToList();

            // Validate and extract user ID
            var userIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
            long userId = 0;
            bool isValidUserId = false;


            var ssoUserIdClaim = user.FindFirstValue(Constant.SessionField.UserId);
            if (!string.IsNullOrEmpty(ssoUserIdClaim))
            {
                isValidUserId = long.TryParse(ssoUserIdClaim, out userId);
            }


            if (!isValidUserId)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized: Invalid User ID");
                return;
            }

            // Set user details in the current user service
            currentUserService.UserId = userId;
            currentUserService.CreateBaseDTO.CreatedById = userId;
            currentUserService.ModifyBaseDTO.ModifiedById = userId;
            currentUserService.DeleteBaseDTO.ModifiedById = userId;
            currentUserService.ClientIPAddress = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            currentUserService.DeviceType = clientInfo.Device.Family ?? "Unknown";
            currentUserService.BrowserName = clientInfo.UA.Family ?? "Unknown";
            currentUserService.UserName = user.FindFirstValue(Constant.SessionField.FullName) ?? string.Empty;

            await _next(context);
        }
    }

    public static class ClientInfoMiddlewareExtensions
    {
        public static IApplicationBuilder UseClientInfoMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ClientInfoMiddleware>();
        }
    }
}
