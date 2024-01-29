using BotickAPI.Application.Common.Interfaces;

namespace WebApi.IntegrationTests.Common.DummyServices
{
    public class DummyCurrentUserService : ICurrentUserService
    {
        public string Email { get; set; } = "user@user.pl";
        public bool IsAuthenticated { get; set; } = true;
    }
}
