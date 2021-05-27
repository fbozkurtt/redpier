using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.Web.UI.Interfaces
{
    public interface IEndpointService
    {
        Task<SystemInfoResponse> GetSystemInfo();
    }
}
