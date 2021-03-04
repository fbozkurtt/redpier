using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Containers
{
    public class StartContainerCommand : IRequest<bool>
    {
        public string Id { get; set; }

        public ContainerStartParameters Parameters { get; set; }
    }
    public class StartContainerCommandHandler : IRequestHandler<StartContainerCommand, bool>
    {
        private readonly IDockerClient _client;

        public StartContainerCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<bool> Handle(StartContainerCommand request, CancellationToken cancellationToken)
        {
            await _client.Containers.StartContainerAsync(
                request.Id,
                request.Parameters ??= new ContainerStartParameters(),
                cancellationToken);

            return true;
        }
    }
}
