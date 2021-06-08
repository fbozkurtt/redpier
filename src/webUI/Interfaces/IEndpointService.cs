using Docker.DotNet.Models;
using Redpier.Web.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Endpoint = Redpier.Web.UI.ViewModels.Endpoint;

namespace Redpier.Web.UI.Interfaces
{
    public interface IEndpointService
    {
        Task<SystemInfoResponse> GetSystemInfoAsync();

        Task<PaginatedListViewModel<Endpoint>> GetEndpointsAsync(int pageNumber = 1, int pageSize = 5, bool all = true);

        Task<bool> PingAsync();

        Task SetSelectedEndpointAsync(Endpoint endpoint);

        Task<Endpoint> GetSelectedEndpointAsync();
    }
}
