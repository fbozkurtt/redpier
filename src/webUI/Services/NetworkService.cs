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

        public async Task<bool> ConnectAsync(string id, NetworkConnectParameters parameters)
        {
            try
            {
                var request = new
                {
                    Id = id,
                    Parameters = parameters
                };

                var response = await _httpClient.PutAsJsonAsync($"/api/network/connect", request);

                return response.IsSuccessStatusCode;
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            return false;
        }

        public async Task<bool> DisconnectAsync(string id, string containerId, bool force = false)
        {
            try
            {
                var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Put,
                    $"/api/network/disconnect?id={id}&containerId={containerId}&force={force}"));

                return response.IsSuccessStatusCode;
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            return false;
        }
    }
}
