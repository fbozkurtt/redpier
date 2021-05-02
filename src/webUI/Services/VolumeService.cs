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
    public class VolumeService : IVolumeService
    {
        private readonly HttpClient _httpClient;

        public VolumeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<VolumeResponse> CreateAsync(VolumesCreateParameters parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<List<VolumeResponse>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/volume");

                if (response.IsSuccessStatusCode)
                {
                    var result =  await response.Content.ReadFromJsonAsync<VolumesListResponse>();

                    return result.Volumes.ToList();
                }
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            catch (Exception)
            {
                throw;
            }

            return null;
        }

        public Task<VolumeResponse> InspectAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<VolumesPruneResponse> PruneAsync(VolumesPruneParameters parameters)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(string name, bool? force)
        {
            throw new NotImplementedException();
        }
    }
}
