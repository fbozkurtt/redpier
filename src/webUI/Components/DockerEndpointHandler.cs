using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

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
            var endpoint = await _localStorage.GetItemAsync<Guid>("selectedEndpointId");

            if (endpoint != default)
            {
                var uriBuilder = new UriBuilder(request.RequestUri);

                if (string.IsNullOrEmpty(uriBuilder.Query))
                {
                    uriBuilder.Query = $"endpoint={endpoint}";
                }
                else
                {
                    uriBuilder.Query = $"{uriBuilder.Query}&endpoint={endpoint}";
                }
                request.RequestUri = uriBuilder.Uri;
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
