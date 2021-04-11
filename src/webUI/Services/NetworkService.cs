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
    public class NetworkService : INetworkService
    {
        private readonly HttpClient _httpClient;

        public NetworkService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<NetworkResponse>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/network");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<NetworkResponse>>();
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
