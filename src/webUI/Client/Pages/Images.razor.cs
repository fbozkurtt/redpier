using Docker.DotNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Redpier.WebUI.Client.Pages
{
    [Authorize]
    public partial class Images : ComponentBase
    {

        [Inject]
        public HttpClient Client { get; set; }

        public IList<ImagesListResponse> ImagesList { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //containers = await Http.GetFromJsonAsync<IList<ContainerListResponse>>("api/Container");
            var request = new
            {
                Parameters = new ImagesListParameters()
                {
                    All = true
                }
            };
            using var response = await Client.PostAsJsonAsync("api/Image", request);

            ImagesList = await response.Content.ReadFromJsonAsync<IList<ImagesListResponse>>();
        }
    }
}
