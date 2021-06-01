using Docker.DotNet.Models;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Redpier.Web.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Redpier.Web.UI.ViewModels;

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

        public async Task<ImageInspectResponse> Inspect(string name)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/image/inspect?name={name}");

                return await response.Content.ReadFromJsonAsync<ImageInspectResponse>();
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            return null;
        }

        public async Task<IList<ImageHistoryResponse>> GetHistory(string name)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/image/history?name={name}");

                return await response.Content.ReadFromJsonAsync<IList<ImageHistoryResponse>>();
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            return null;
        }

        public async Task RemoveAsync(string name, bool force = false)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/image?name={name}&force={force}");

                if (!response.IsSuccessStatusCode)
                {
                    var exception = await response.Content.ReadFromJsonAsync<ApiException>();
                    throw new Exception(exception.Detail);
                }
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
        }

        public async Task<bool> Pull(string imageName)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"/api/image/create", new
                {
                    Parameters = new ImagesCreateParameters()
                    {
                        FromImage = imageName
                    }
                });

                return response.IsSuccessStatusCode;
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            return false;
        }

        public Task<bool> Push(ImagePushParameters parameters)
        {
            throw new NotImplementedException();
        }

        public async Task TagAsync(string name, string repoName, string tag)
        {
            try
            {
                var response = await _httpClient.PutAsync(
                    $"/api/image/tag?name={name}" +
                    $"&repositoryName={repoName}" +
                    $"&tag={tag}",
                    null);

                if (!response.IsSuccessStatusCode)
                {
                    var exception = await response.Content.ReadFromJsonAsync<ApiException>();
                    throw new Exception(exception.Detail);
                }
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
        }
    }
}
