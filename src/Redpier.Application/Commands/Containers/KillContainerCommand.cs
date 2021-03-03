using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Containers
{
    public class KillContainerCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public ContainerKillParameters Parameters { get; set; }
    }

    public class KillContainersCommandHandler : IRequestHandler<KillContainerCommand, bool>
    {
        private readonly IDockerClient _client;

        public KillContainersCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<bool> Handle(KillContainerCommand request, CancellationToken cancellationToken)
        {
            await _client.Containers.KillContainerAsync(
                request.Id, 
                request.Parameters,
                cancellationToken);
            return true;
        }
    }
}
