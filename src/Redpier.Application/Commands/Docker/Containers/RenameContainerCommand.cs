using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Containers
{
    public class RenameContainerCommand : IRequest
    {
        public string Id { get; set; }
        public ContainerRenameParameters Parameters { get; set; }
    }

    public class RenameContainerCommandQuery : IRequestHandler<RenameContainerCommand>
    {
        private readonly IDockerClient _client;

        public RenameContainerCommandQuery(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(RenameContainerCommand request, CancellationToken cancellationToken)
        {
            await _client.Containers.RenameContainerAsync(
                request.Id,
                request.Parameters,
                cancellationToken);

            return Unit.Value;
        }
    }
}
