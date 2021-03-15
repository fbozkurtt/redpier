using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Redpier.Shared.Constants;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Swarm
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class JoinSwarmCommand : IRequest
    {
        public SwarmJoinParameters Parameters { get; set; }
    }

    public class JoinSwarmCommandHandler : IRequestHandler<JoinSwarmCommand>
    {
        private readonly IDockerClient _client;

        public JoinSwarmCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(JoinSwarmCommand request, CancellationToken cancellationToken)
        {
            await _client.Swarm.JoinSwarmAsync(
                request.Parameters,
                cancellationToken);

            return Unit.Value;
        }
    }
}
