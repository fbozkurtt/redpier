using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Volumes
{
    public class PruneVolumesCommand : IRequest<VolumesPruneResponse>
    {
        public VolumesPruneParameters Parameters { get; set; }
    }

    public class PruneVolumesCommandHandler : IRequestHandler<PruneVolumesCommand, VolumesPruneResponse>
    {
        private readonly IDockerClient _client;

        public PruneVolumesCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<VolumesPruneResponse> Handle(PruneVolumesCommand request, CancellationToken cancellationToken)
        {
            return await _client.Volumes.PruneAsync(
                request.Parameters,
                cancellationToken);
        }
    }
}
