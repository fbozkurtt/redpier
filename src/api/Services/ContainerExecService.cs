using Docker.DotNet;
using Redpier.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.Web.API.Services
{
    public class ContainerExecService : IContainerExecService
    {
        private readonly List<MultiplexedStream> _sessions;
        private readonly IDockerClient _client;

        public ContainerExecService(IDockerClient client)
        {
            _sessions = new List<MultiplexedStream>();
            _client = client;
        }

        public Task AddAsync(MultiplexedStream stream)
        {
            _sessions.Add(stream);

            return Task.CompletedTask;
        }

        public Task<MultiplexedStream> GetExecSessionAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAsync(string id)
        {
            var session = await GetExecSessionAsync(id);
            session.Dispose();
            _sessions.Remove(session);
        }
    }
}
