using Docker.DotNet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Volumes
{
    [Authorize(Roles = "Admin")]
    public class RemoveVolumeCommand : IRequest
    {
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
