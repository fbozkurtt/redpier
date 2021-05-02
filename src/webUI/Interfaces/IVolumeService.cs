using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.Web.UI.Interfaces
{
    public interface IVolumeService : IServiceBase<VolumeResponse>
    {
        Task<VolumeResponse> InspectAsync(string name);

        Task<VolumeResponse> CreateAsync(VolumesCreateParameters parameters);

        Task<VolumesPruneResponse> PruneAsync(VolumesPruneParameters parameters);

        Task<bool> RemoveAsync(string name, bool? force);
    }
}
