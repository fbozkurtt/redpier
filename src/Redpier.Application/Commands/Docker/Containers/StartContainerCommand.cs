using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Containers
{
    public class StartContainerCommand : IRequest
    {
        public string Id { get; set; }

        public ContainerStartParameters Parameters { get; set; }
    }
    public class StartContainerCommandHandler : IRequestHandler<StartContainerCommand>
    {
        private readonly IDockerClient _client;

        public StartContainerCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(StartContainerCommand request, CancellationToken cancellationToken)
        {
            await _client.Containers.StartContainerAsync(
                request.Id,
                request.Parameters ??= new ContainerStartParameters(),
                cancellationToken);

            return Unit.Value;
        }
    }
}
