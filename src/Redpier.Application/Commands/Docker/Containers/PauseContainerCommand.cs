using Docker.DotNet;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Containers
{
    public class PauseContainerCommand : IRequest
    {
        public string Id { get; set; }
    }

    public class PauseContainerCommandHandler : IRequestHandler<PauseContainerCommand>
    {
        private readonly IDockerClient _client;

        public PauseContainerCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(PauseContainerCommand request, CancellationToken cancellationToken)
        {
            await _client.Containers.PauseContainerAsync(
                request.Id,
                cancellationToken);

            return Unit.Value;
        }
    }
}
