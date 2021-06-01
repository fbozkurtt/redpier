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
using Redpier.Web.UI.Helpers;

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

        public async Task<bool> StartAsync(string containerId)
        {
            try
            {
                var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Put, $"/api/container/start?Id={containerId}"));
                return response.IsSuccessStatusCode;
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            return false;
        }

        public async Task<bool> StopAsync(string containerId)
        {
            try
            {
                var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Put, $"/api/container/stop?Id={containerId}"));
                return response.IsSuccessStatusCode;
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            return false;
        }

        public async Task<bool> PauseAsync(string containerId)
        {
            try
            {
                var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Put, $"/api/container/pause?Id={containerId}"));
                return response.IsSuccessStatusCode;
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            return false;
        }

        public async Task<bool> UnpauseAsync(string containerId)
        {
            try
            {
                var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Put, $"/api/container/unpause?Id={containerId}"));
                return response.IsSuccessStatusCode;
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            return false;
        }

        public async Task<bool> RestartAsync(string containerId)
        {
            try
            {
                var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Put, $"/api/container/restart?Id={containerId}"));
                return response.IsSuccessStatusCode;
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            return false;
        }

        public async Task<bool> KillAsync(string containerId)
        {
            try
            {
                var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Put, $"/api/container/kill?Id={containerId}"));
                return response.IsSuccessStatusCode;
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            return false;
        }

        public async Task<bool> RemoveAsync(string containerId, bool removeVolumes = false)
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

        public Task<bool> RenameAsync(string containerId, string name)
        {
            throw new NotImplementedException();
        }

        public async Task<ContainerInspectResponse> InspectAsync(string containerId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/container/inspect?Id={containerId}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ContainerInspectResponse>();
                }
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            return null;
        }

        public Task<bool> PruneAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ContainerUpdateResponse> UpdateAsync(string containerId, ContainerUpdateParameters parameters)
        {
            try
            {
                var request = new
                {
                    Id = containerId,
                    Parameters = parameters
                };

                var response = await _httpClient.PutAsJsonAsync($"/api/container", request);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ContainerUpdateResponse>();
                }
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            return null;
        }

        public async Task<CreateContainerResponse> CreateAsync(CreateContainerParameters parameters)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"/api/container", new { Parameters = parameters });

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CreateContainerResponse>();
                }
                var exception = await response.Content.ReadFromJsonAsync<ApiException>();
                throw new Exception(exception.Detail);
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            return null;
        }

        public async Task<string[]> GetLogs(string containerId, ContainerLogsParameters parameters)
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    $"/api/container/logs?" +
                    $"ShowStdout={parameters.ShowStdout.Value}" +
                    $"&ShowStderr={parameters.ShowStderr.Value}" +
                    $"&Since={parameters.Since}" +
                    $"&Timestamps={parameters.Timestamps.Value}" +
                    $"&Tail={parameters.Tail}" +
                    $"&Id={containerId}");

                if (response.IsSuccessStatusCode)
                {
                    var logs = await response.Content.ReadFromJsonAsync<string>();
                    Console.WriteLine(logs);
                    return LogHelper.FormatLogs(logs, true, parameters.Timestamps.Value);
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
