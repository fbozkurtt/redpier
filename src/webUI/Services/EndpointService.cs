using Blazored.LocalStorage;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Redpier.Web.UI.Interfaces;
using Redpier.Web.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Endpoint = Redpier.Web.UI.ViewModels.Endpoint;

namespace Redpier.Web.UI.Services
{
    public class EndpointService : IEndpointService
    {
        private readonly HttpClient _httpClient;

        private ILocalStorageService _localStorage;

        public EndpointService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<PaginatedListViewModel<Endpoint>> GetEndpointsAsync(int pageNumber = 1, int pageSize = 5, bool all = true)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/endpoint?" +
                    $"pageNumber={pageNumber}" +
                    $"&pageSize={pageSize}" +
                    $"&all={true}");

                if (!response.IsSuccessStatusCode)
                {
                    var exception = await response.Content.ReadFromJsonAsync<ApiException>();
                    throw new Exception(exception.Detail);

                }

                return await response.Content.ReadFromJsonAsync<PaginatedListViewModel<Endpoint>>();
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }

            return default;
        }

        public async Task<Endpoint> GetSelectedEndpointAsync()
            => await _localStorage.GetItemAsync<Endpoint>("selectedEndpoint");

        public async Task<SystemInfoResponse> GetSystemInfoAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/endpoint/info");

                if (!response.IsSuccessStatusCode)
                {
                    var exception = await response.Content.ReadFromJsonAsync<ApiException>();
                    throw new Exception(exception.Detail);

                }

                return await response.Content.ReadFromJsonAsync<SystemInfoResponse>();
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }

            return default;
        }

        public async Task<bool> PingAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/endpoint/ping");

                return response.IsSuccessStatusCode;
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }

            return false;
        }

        public async Task SetSelectedEndpointAsync(Endpoint endpoint)
            => await _localStorage.SetItemAsync("selectedEndpoint", endpoint);
    }
}
