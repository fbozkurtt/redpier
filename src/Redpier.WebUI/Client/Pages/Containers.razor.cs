using Docker.DotNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Timers;

namespace Redpier.WebUI.Client.Pages
{
    [Authorize]
    public partial class Containers : ComponentBase, IDisposable
    {
        public Timer Timer = new Timer(1000);

        [Inject]
        public HttpClient Client { get; set; }

        public IList<ContainerListResponse> ContainerList { get; set; }
        protected override void OnInitialized()
        {
            Timer.Elapsed += (sender, eventArgs) => OnTimerCallback();
            Timer.Start();
        }
        private void OnTimerCallback()
        {
            _ = InvokeAsync(async () =>
            {
                await FetchData();
            });
        }

        protected override async Task OnInitializedAsync()
        {
            //containers = await Http.GetFromJsonAsync<IList<ContainerListResponse>>("api/Container");
            try
            {
                await FetchData();
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
        }

        protected async Task FetchData()
        {
            var body = new
            {
                Parameters = new ContainersListParameters()
                {
                    All = true
                }
            };

            var response = await Client.PostAsJsonAsync("api/Container", body);

            if (response.IsSuccessStatusCode)
            {
                ContainerList = await response.Content.ReadFromJsonAsync<IList<ContainerListResponse>>();
            }

            StateHasChanged();
        }

        protected async Task Start(string id)
        {
            var request = new
            {
                Id = id
            };

            await Client.PutAsJsonAsync("api/Container/Start", request);
            await FetchData();
        }

        protected async Task Stop(string id)
        {
            var request = new
            {
                Id = id
            };

            await Client.PutAsJsonAsync("api/Container/Stop", request);
            await FetchData();
        }

        public void Dispose()
        {
            Timer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
