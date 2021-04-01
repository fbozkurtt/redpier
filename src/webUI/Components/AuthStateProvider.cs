using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Redpier.Web.UI.Components
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;
        private readonly AuthenticationState _anonymous;

        public AuthStateProvider(ILocalStorageService localStorage, HttpClient  httpClient)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("JWT");

            if (string.IsNullOrWhiteSpace(token))
                return _anonymous;

            var expires = await _localStorage.GetItemAsync<DateTime>("JWT.Expires");

            if (expires < DateTime.UtcNow)
            {
                await _localStorage.RemoveItemAsync("JWT");
                await _localStorage.RemoveItemAsync("JWT.Expires");
                return _anonymous;
            }

            var identity = new ClaimsIdentity("Bearer");

            var user = new ClaimsPrincipal(identity);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return new AuthenticationState(user);
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
