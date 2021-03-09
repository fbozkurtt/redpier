using Docker.DotNet;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Volumes
{
    public class RemoveVolumeCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public bool? Force { get; set; }
    }

    public class RemoveVolumeCommandHandler : IRequestHandler<RemoveVolumeCommand, bool>
    {
        private readonly IDockerClient _client;

        public RemoveVolumeCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<bool> Handle(RemoveVolumeCommand request, CancellationToken cancellationToken)
        {
            await _client.Volumes.RemoveAsync(
                request.Name,
                request.Force,
                cancellationToken);

            return true;
        }
    }
}
