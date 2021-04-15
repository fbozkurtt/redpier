using Redpier.Web.UI.Shared.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Components;
using Redpier.Web.UI.Interfaces;

namespace Redpier.Web.UI.Pages.Containers
{
    public partial class Add
    {
        [Inject]
        public IImageService ImageService { get; set; }

        [Inject]
        public IContainerService ContainerService { get; set; }

        CreateContainerParameters Model { get; set; } = new CreateContainerParameters()
        {
            Cmd = new List<string>() { "" },
            Entrypoint = new List<string>() { "" },
            NetworkingConfig = new NetworkingConfig()
            {
                EndpointsConfig = new Dictionary<string, EndpointSettings>() { { "bridge", new EndpointSettings() {
                    IPAMConfig = new EndpointIPAMConfig() 
                    {
                        IPv4Address="",
                        IPv6Address=""
                    }
                } } }
            }
        };

        public List<string> LocalImages { get; set; } = new List<string>();

        public bool OverrideDefaultCommand { get; set; } = false;

        public bool OverrideDefaultEntryPoint { get; set; } = false;

        public bool PullImage { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            var response = await ImageService.GetAllAsync();
            LocalImages = response.Select(i => i.RepoTags).Aggregate((a, b) => a.Concat(b).ToList()).ToList();
            PageLoaded = true;
        }

        private async Task HandleValidSubmit()
        {
            try
            {
                IsBusy = true;
                var result = await ContainerService.Create(Model);
                if (result.ID != null)
                    ToastService.ShowSuccess($"Created container {result.ID}");
                if (result.Warnings.Count > 0)
                    ToastService.ShowWarning($"{result.Warnings.Aggregate((a, b) => a + "\n" + b)}");
            }
            catch (Exception ex)
            {
                ToastService.ShowError($"An error occured while creating container {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task<IEnumerable<string>> SearchImages(string searchText)
        {
            return await Task.FromResult(LocalImages.Where(i => i.ToLowerInvariant().Contains(searchText.ToLowerInvariant())).ToList());
        }
    }
}
