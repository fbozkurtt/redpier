using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Containers
{
    public class StopContainerCommand : IRequest
    {
        public string Id { get; set; }

        public ContainerStopParameters Parameters { get; set; }
    }
    public class StopContainerCommandHandler : IRequestHandler<StopContainerCommand>
    {
        private readonly IDockerClient _client;

        public StopContainerCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(StopContainerCommand request, CancellationToken cancellationToken)
        {
            await _client.Containers.StopContainerAsync(
                request.Id,
                request.Parameters ??= new ContainerStopParameters(),
                cancellationToken);

            return Unit.Value;
        }
    }
}
