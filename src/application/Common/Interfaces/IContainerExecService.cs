using Docker.DotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redpier.Application.Common.Interfaces
{
    public interface IContainerExecService
    {
        Task<MultiplexedStream> GetExecSessionAsync(string id);

        Task AddAsync(MultiplexedStream stream);

        Task RemoveAsync(string id);
    }
}
