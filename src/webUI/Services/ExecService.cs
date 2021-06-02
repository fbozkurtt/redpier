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

namespace Redpier.Web.UI.Services
{
    public class ExecService : IExecService
    {
        private readonly HttpClient _httpClient;

        public ExecService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> CreateAsync(string containerId, ContainerExecCreateParameters parameters)
        {
            try
            {
                var body = new
                {
                    ContainerId = containerId,
                    Parameters = parameters
                };

                var content = JsonContent.Create(body);

                var response = await _httpClient.PostAsync("/api/exec", content);

                if (!response.IsSuccessStatusCode)
                {
                    var exception = await response.Content.ReadFromJsonAsync<ApiException>();
                    throw new Exception(exception.Detail);
                }

                return await response.Content.ReadFromJsonAsync<string>();
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }

            return string.Empty;
        }
    }
}
