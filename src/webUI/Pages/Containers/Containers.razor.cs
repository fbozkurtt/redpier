using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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
