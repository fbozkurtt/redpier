using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.Web.UI.Interfaces
{
    public interface INetworkService : IServiceBase<NetworkResponse>
    {
        public Task<bool> ConnectAsync(string id, NetworkConnectParameters parameters);
        public Task<bool> DisconnectAsync(string id, string containerId, bool force = false);
    }
}
