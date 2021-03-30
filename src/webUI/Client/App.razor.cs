using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;

namespace Redpier.WebUI.Client
{
    public partial class App : ComponentBase, IDisposable
    {
        public bool DockerStatus { get; private set; } = false;

        public Timer Timer = new Timer(1000);

        [Inject]
        public HttpClient Client { get; set; }

        protected override void OnInitialized()
        {
            Timer.Elapsed += (sender, eventArgs) => OnTimerCallback();
            Timer.Start();
        }

        private void OnTimerCallback()
        {
            _ = InvokeAsync(async () =>
            {
                await PingDocker();
            });
        }

        protected async Task PingDocker()
        {
            var response = await Client.GetAsync("api/Docker");

            DockerStatus = response.IsSuccessStatusCode;

            StateHasChanged();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
