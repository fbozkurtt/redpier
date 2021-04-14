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

        CreateContainerParameters Model { get; set; } = new CreateContainerParameters()
        {
            Cmd = new List<string>() { "" },
            Entrypoint = new List<string>() { "" }
        };

        public List<string> LocalImages { get; set; } = new List<string>(); 

        public bool OverrideDefaultCommand { get; set; } = false;

        public bool OverrideDefaultEntryPoint { get; set; } = false;

        public bool PullImage{ get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            var response = await ImageService.GetAllAsync();
            var asd = response.Select(i => i.RepoTags).ToList();
            PageLoaded = true;
        }

        private async Task HandleValidSubmit()
        {

        }

        private async Task<IEnumerable<string>> SearchImages(string searchText)
        {
            return await Task.FromResult(LocalImages.Where(i => i.ToLowerInvariant().Contains(searchText.ToLowerInvariant())).ToList());
        }
    }
}
