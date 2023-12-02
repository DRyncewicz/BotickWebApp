using System.Security.Claims;
using BotickAPI.Application.Common.Interfaces;
using IdentityModel;

namespace BotickAPI.Service
{
    public class CurrentUserService : ICurrentUserService
    {
        public string Email { get; set; }

        public bool IsAuthenticated { get; set; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var email = httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtClaimTypes.Email);

            Email = email;

            IsAuthenticated = !string.IsNullOrEmpty(email);
        }
    }
}
