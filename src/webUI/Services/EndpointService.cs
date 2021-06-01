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

        public EndpointService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginatedListViewModel<Endpoint>> GetEndpointsAsync(int pageNumber = 1, int pageSize = 5, bool all = true)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/endpoint?pageNumber={pageNumber}&pageSize={pageSize}&all={true}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<PaginatedListViewModel<Endpoint>>();
                }
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            return null;
        }

        public async Task<SystemInfoResponse> GetSystemInfo()
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/endpoint/info");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<SystemInfoResponse>();
                }
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            return null;
        }
    }
}
