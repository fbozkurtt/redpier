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
    public class UpdateNodeCommand : IRequest
    {
        [Required]
        public string Endpoint { get; set; }

        [Required]
        public string Id { get; set; }

        public ulong Version { get; set; }

        public NodeUpdateParameters Parameters { get; set; }
    }

    public class UpdateNodeCommandHandler : IRequestHandler<UpdateNodeCommand>
    {
        private readonly IDockerClient _client;

        public UpdateNodeCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(UpdateNodeCommand request, CancellationToken cancellationToken)
        {
            await _client.Swarm.UpdateNodeAsync(
                request.Id,
                request.Version,
                request.Parameters,
                cancellationToken);

            return Unit.Value;
        }
    }
}
