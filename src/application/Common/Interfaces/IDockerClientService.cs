using Docker.DotNet;
using Redpier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redpier.Application.Common.Interfaces
{
    public interface IDockerClientService
    {
        Task<IDockerClient> CreateClient();
    }
}
