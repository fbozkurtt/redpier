using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Redpier.Shared.Constants;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Swarm
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class UpdateSwarmCommand : IRequest
    {
        [Required]
        public string Endpoint { get; set; }

        public SwarmUpdateParameters Parameters { get; set; }
    }

    public class UpdateSwarmCommandHandler : IRequestHandler<UpdateSwarmCommand>
    {
        private readonly IDockerClient _client;

        public UpdateSwarmCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(UpdateSwarmCommand request, CancellationToken cancellationToken)
        {
            await _client.Swarm.UpdateSwarmAsync(
                   request.Parameters,
                   cancellationToken);

            return Unit.Value;
        }
    }
}
