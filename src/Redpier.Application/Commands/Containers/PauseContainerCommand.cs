using Docker.DotNet;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Containers
{
    public class PauseContainerCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }

    public class PauseContainerCommandHandler : IRequestHandler<PauseContainerCommand, bool>
    {
        private readonly IDockerClient _client;

        public PauseContainerCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<bool> Handle(PauseContainerCommand request, CancellationToken cancellationToken)
        {
            await _client.Containers.PauseContainerAsync(
                request.Id,
                cancellationToken);

            return true;
        }
    }
}
