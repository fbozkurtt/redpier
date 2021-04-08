using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Web.UI.Interfaces
{
    public interface IContainerService
    {
        Task<List<ContainerListResponse>> GetAllAsync();

        Task<bool> StartContainerAsync(string containerId);

        Task<bool> StopContainerAsync(string containerId);

        Task<bool> RemoveContainerAsync(string containerId, bool removeVolumes);

        Task<bool> PauseContainerAsync(string containerId);

        Task<bool> UnpauseContainerAsync(string containerId);

        Task<bool> RestartContainerAsync(string containerId);

        Task<bool> RenameContainerAsync(string containerId, string name);

        Task<bool> PruneContainersAsync();
    }
}
