using Docker.DotNet.Models;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Redpier.Web.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Redpier.Web.UI.Services
{
    public class EndpointService : IEndpointService
    {
        private readonly HttpClient _httpClient;

        public EndpointService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
