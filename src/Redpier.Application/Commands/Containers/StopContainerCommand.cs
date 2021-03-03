using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Containers
{
    public class StopContainerCommand : IRequest<bool>
    {
        public string Id { get; set; }

        public ContainerStopParameters Parameters { get; set; }
    }
    public class StopContainerCommandHandler : IRequestHandler<StopContainerCommand, bool>
    {
        private readonly IDockerClient _client;

        public StopContainerCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<bool> Handle(StopContainerCommand request, CancellationToken cancellationToken)
        {
            await _client.Containers.StopContainerAsync(
                request.Id,
                request.Parameters,
                cancellationToken);

            return true;
        }
    }
}
