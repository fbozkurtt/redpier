using Blazored.LocalStorage;
using Blazored.Toast;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Redpier.Web.UI.Components;
using Redpier.Web.UI.Interfaces;
using Redpier.Web.UI.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Redpier.Web.UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddBlazoredLocalStorage(config =>
                config.JsonSerializerOptions.WriteIndented = true);

            //builder.Services.AddScoped(sp => new HttpClient
            //{
            //    BaseAddress = new Uri("https://localhost:5000")
            //});

            builder.Services.AddScoped<AuthMessageHandler>();
            builder.Services.AddScoped<DockerEndpointHandler>();
            builder.Services.AddScoped<AccessTokenProvider>();
            builder.Services.AddScoped<IAccessTokenProvider>(sp => sp.GetRequiredService<AccessTokenProvider>());

            builder.Services.AddHttpClient("api",
                    client => client.BaseAddress = new Uri("https://localhost:5000"))
                .AddHttpMessageHandler<AuthMessageHandler>()
                .AddHttpMessageHandler<DockerEndpointHandler>();

            builder.Services.AddHttpClient("apinoauth",
                    client => client.BaseAddress = new Uri("https://localhost:5000"));

            builder.Services.AddTransient(sp =>
                sp.GetRequiredService<IHttpClientFactory>().CreateClient("api"));

            builder.Services.AddScoped<IContainerService, ContainerService>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<INetworkService, NetworkService>();
            builder.Services.AddScoped<IVolumeService, VolumeService>();
            builder.Services.AddScoped<IEndpointService, EndpointService>();
            builder.Services.AddScoped<IExecService, ExecService>();

            builder.Services.AddScoped<IServiceBase<ContainerListResponse>>(sp => sp.GetRequiredService<IContainerService>());
            builder.Services.AddScoped<IServiceBase<ImagesListResponse>>(sp => sp.GetRequiredService<IImageService>());
            builder.Services.AddScoped<IServiceBase<NetworkResponse>>(sp => sp.GetRequiredService<INetworkService>());
            builder.Services.AddScoped<IServiceBase<VolumeResponse>>(sp => sp.GetRequiredService<IVolumeService>());

            builder.Services.AddBlazoredToast();

            //builder.Services.AddOptions();

            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

            await builder.Build().RunAsync();
        }
    }
}
