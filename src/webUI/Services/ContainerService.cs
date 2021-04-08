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
    public class ContainerService : IContainerService
    {
        private readonly HttpClient _httpClient;

        public ContainerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ContainerListResponse>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/container");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<ContainerListResponse>>();
                }
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            return null;
        }

        public Task<bool> PauseContainerAsync(string containerId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PruneContainersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveContainerAsync(string containerId, bool removeVolumes = false)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/container?Id={containerId}&removeVolumes={removeVolumes}");
                return response.IsSuccessStatusCode;
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            return false;
        }

        public Task<bool> RenameContainerAsync(string containerId, string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RestartContainerAsync(string containerId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> StartContainerAsync(string containerId)
        {
            try
            {
                var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Put, $"/api/container/start?Id={containerId}"));
                return response.IsSuccessStatusCode;
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
                return false;
            }
        }

        public async Task<bool> StopContainerAsync(string containerId)
        {
            try
            {
                var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Put, $"/api/container/stop?Id={containerId}"));
                return response.IsSuccessStatusCode;
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
                return false;
            }
        }

        public Task<bool> UnpauseContainerAsync(string containerId)
        {
            throw new NotImplementedException();
        }
    }
}
