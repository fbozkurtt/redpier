using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Threading.Tasks;

namespace Redpier.Web.UI.Components
{
    public class AccessTokenProvider : IAccessTokenProvider
    {
        private readonly ILocalStorageService _localStorage;

        public AccessTokenProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async ValueTask<AccessTokenResult> RequestAccessToken()
        {
            var token = await _localStorage.GetItemAsync<string>("JWT");

            if (string.IsNullOrWhiteSpace(token))
                return new AccessTokenResult(AccessTokenResultStatus.RequiresRedirect, null, "/login");

            var expires = await _localStorage.GetItemAsync<DateTime>("JWT.Expires");

            if (expires < DateTime.UtcNow)
            {
                await _localStorage.RemoveItemAsync("JWT");
                await _localStorage.RemoveItemAsync("JWT.Expires");

                return new AccessTokenResult(AccessTokenResultStatus.RequiresRedirect, null, "/login");
            }

            var accessToken = new AccessToken() { Value = token, Expires = expires };

            return new AccessTokenResult(AccessTokenResultStatus.Success, accessToken, "/");
        }

        public async ValueTask<AccessTokenResult> RequestAccessToken(AccessTokenRequestOptions options)
        {
            return await RequestAccessToken();
        }
    }
}
