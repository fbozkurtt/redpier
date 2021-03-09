using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Containers
{
    public class PruneContainersCommand : IRequest<ContainersPruneResponse>
    {
        public ContainersPruneParameters Parameters { get; set; }
    }

    public class PruneContainersCommandHandler : IRequestHandler<PruneContainersCommand, ContainersPruneResponse>
    {
        private readonly IDockerClient _client;

        public PruneContainersCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<ContainersPruneResponse> Handle(PruneContainersCommand request, CancellationToken cancellationToken)
        {
            return await _client.Containers.PruneContainersAsync(
                request.Parameters,
                cancellationToken);
        }
    }
}
