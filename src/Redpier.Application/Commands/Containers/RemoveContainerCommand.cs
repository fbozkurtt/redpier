using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Containers
{
    public class RemoveContainerCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public ContainerRemoveParameters Parameters { get; set; }
    }

    public class RemoveContainerCommandHandler : IRequestHandler<RemoveContainerCommand, bool>
    {
        private readonly IDockerClient _client;

        public RemoveContainerCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<bool> Handle(RemoveContainerCommand request, CancellationToken cancellationToken)
        {
            await _client.Containers.RemoveContainerAsync(request.Id,
                request.Parameters,
                cancellationToken);

            return true;
        }
    }
}
