using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Redpier.Web.UI.Components
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly AuthenticationState _anonymous;
        private readonly AccessTokenProvider _tokenProvider;

        public AuthStateProvider(ILocalStorageService localStorage,
            HttpClient httpClient,
            AccessTokenProvider tokenProvider)
        {
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            _tokenProvider = tokenProvider;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var result = await _tokenProvider.RequestAccessToken();

            if (result.Status == AccessTokenResultStatus.Success)
            {
                var identity = new ClaimsIdentity("Bearer");

                var user = new ClaimsPrincipal(identity);

                return new AuthenticationState(user);
            }

            return _anonymous;
        }

        public void NotifyAuthenticationStateChanged(string username)
        {
            var identity = new ClaimsIdentity("Bearer");

            identity.AddClaim(new Claim(ClaimTypes.Name, username));

            var user = new ClaimsPrincipal(identity);

            var authState = Task.FromResult(new AuthenticationState(user));

            base.NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyAuthenticationStateChanged()
        {
            base.NotifyAuthenticationStateChanged(Task.FromResult(_anonymous));
        }
    }
}
