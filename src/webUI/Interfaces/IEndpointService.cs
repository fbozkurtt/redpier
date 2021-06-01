using Docker.DotNet.Models;
using Redpier.Web.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.Web.UI.Interfaces
{
    public interface IEndpointService
    {
        Task<SystemInfoResponse> GetSystemInfo();

        Task<PaginatedListViewModel<ViewModels.Endpoint>> GetEndpointsAsync(int pageNumber = 1, int pageSize = 5, bool all = true);
    }
}
