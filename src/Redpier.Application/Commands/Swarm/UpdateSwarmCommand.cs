using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Swarm
{
    public class UpdateSwarmCommand : IRequest<bool>
    {
        public SwarmUpdateParameters Parameters { get; set; }
    }

    public class UpdateSwarmCommandHandler : IRequestHandler<UpdateSwarmCommand, bool>
    {
        private readonly IDockerClient _client;

        public UpdateSwarmCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<bool> Handle(UpdateSwarmCommand request, CancellationToken cancellationToken)
        {
            await _client.Swarm.UpdateSwarmAsync(
                   request.Parameters,
                   cancellationToken);
            return true;
        }
    }
}
