using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Redpier.Web.UI.Components
{
    public class AuthMessageHandler : AuthorizationMessageHandler
    {
        public AuthMessageHandler(
            AccessTokenProvider provider,
            NavigationManager navigation)
            : base(provider, navigation)
        {
            ConfigureHandler(
                authorizedUrls: new[] { "https://localhost:5000" });
        }
    }
}
