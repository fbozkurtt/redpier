using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Containers
{
    public class RestartContainerCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public ContainerRestartParameters Parameters { get; set; }
    }
    public class RestartContainerCommandHandler : IRequestHandler<RestartContainerCommand, bool>
    {
        private readonly IDockerClient _client;

        public RestartContainerCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<bool> Handle(RestartContainerCommand request, CancellationToken cancellationToken)
        {
            await _client.Containers.RestartContainerAsync(
                request.Id,
                request.Parameters,
                cancellationToken);

            return true;
        }
    }
}
