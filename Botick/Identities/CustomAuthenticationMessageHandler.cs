using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Botick.Identities
{
    public class CustomAuthenticationMessageHandler : AuthorizationMessageHandler
    {
        public CustomAuthenticationMessageHandler(IAccessTokenProvider provider, NavigationManager navigationManager) : base(provider, navigationManager)
        {
            ConfigureHandler(new string[]
            {
                "https://localhost:5001"
            });
        }
    }
}
