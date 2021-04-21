using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Web.UI.Interfaces
{
    public interface IContainerService : IServiceBase<ContainerListResponse>
    {
        Task<bool> StartAsync(string containerId);

        Task<bool> StopAsync(string containerId);

        Task<bool> PauseAsync(string containerId);

        Task<bool> UnpauseAsync(string containerId);

        Task<bool> RestartAsync(string containerId);

        Task<bool> KillAsync(string containerId);

        Task<bool> RemoveAsync(string containerId, bool removeVolumes = false);

        Task<bool> RenameAsync(string containerId, string name);

        Task<ContainerInspectResponse> InspectAsync(string containerId);

        Task<bool> PruneAsync();

        Task<ContainerUpdateResponse> UpdateAsync(string containerId, ContainerUpdateParameters parameters);

        Task<CreateContainerResponse> CreateAsync(CreateContainerParameters parameters);
    }
}
