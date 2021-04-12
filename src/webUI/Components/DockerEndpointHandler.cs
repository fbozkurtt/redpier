using Blazored.LocalStorage;
using Redpier.Web.UI.ViewModels;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Web.UI.Components
{
    public class DockerEndpointHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;

        public DockerEndpointHandler(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var endpoint = await _localStorage.GetItemAsync<Endpoint>("selectedEndpoint");

            if (endpoint != default)
            {
                var uriBuilder = new UriBuilder(request.RequestUri);

                if (string.IsNullOrEmpty(uriBuilder.Query))
                {
                    uriBuilder.Query = $"endpoint={Convert.ToBase64String(Encoding.UTF8.GetBytes(endpoint.Uri))}";
                }
                else
                {
                    uriBuilder.Query = $"{uriBuilder.Query}&endpoint={Convert.ToBase64String(Encoding.UTF8.GetBytes(endpoint.Uri))}";
                }
                request.RequestUri = uriBuilder.Uri;
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
