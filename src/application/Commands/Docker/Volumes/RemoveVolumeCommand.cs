using Docker.DotNet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Redpier.Shared.Constants;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Volumes
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class RemoveVolumeCommand : IRequest
    {
        [Required]
        public string Endpoint { get; set; }

        [Required]
        public string Name { get; set; }

        public bool? Force { get; set; }
    }

    public class RemoveVolumeCommandHandler : IRequestHandler<RemoveVolumeCommand>
    {
        private readonly IDockerClient _client;

        public RemoveVolumeCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(RemoveVolumeCommand request, CancellationToken cancellationToken)
        {
            await _client.Volumes.RemoveAsync(
                request.Name,
                request.Force,
                cancellationToken);

            return Unit.Value;
        }
    }
}
