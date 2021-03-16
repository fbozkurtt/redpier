using Docker.DotNet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Redpier.Shared.Constants;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Swarm
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class RemoveNodeCommand : IRequest
    {
        [Required]
        public string Endpoint { get; set; }

        [Required]
        public string Id { get; set; }

        public bool? Force { get; set; }
    }
    public class RemoveNodeCommandHandler : IRequestHandler<RemoveNodeCommand>
    {
        private readonly IDockerClient _client;

        public RemoveNodeCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(RemoveNodeCommand request, CancellationToken cancellationToken)
        {
            await _client.Swarm.RemoveNodeAsync(
                request.Id,
                request.Force.Value,
                cancellationToken);

            return Unit.Value;
        }
    }
}
