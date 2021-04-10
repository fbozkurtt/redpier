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
    public class ImageService : IImageService
    {
        private readonly HttpClient _httpClient;

        public ImageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ImagesListResponse>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/image");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<ImagesListResponse>>();
                }
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            return null;
        }

        public Task<ImageInspectResponse> Inspect(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ImageHistoryResponse>> GetHistory(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Remove(string name, bool force = false)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/image?name={name}&force={force}");

                //Console.WriteLine(await response.Content.ReadFromJsonAsync<IList<IDictionary<string, string>>>());

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
