using Microsoft.AspNetCore.Components;
using System.Net.Http;

namespace Redpier.Web.UI.Pages.Containers
{
    public partial class Containers : ComponentBase
    {
        [Inject]
        public HttpClient Client { get; set; }

        //protected override async Task OnInitializedAsync()
        //{

        //}

        //public async Task FetchData()
        //{

        //}
    }
}
