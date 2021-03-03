using Docker.DotNet;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Containers
{
    public class UnpauseContainerCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }
    public class UnpauseContainerCommandHandler : IRequestHandler<UnpauseContainerCommand, bool>
    {
        private readonly IDockerClient _client;

        public UnpauseContainerCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<bool> Handle(UnpauseContainerCommand request, CancellationToken cancellationToken)
        {
            await _client.Containers.UnpauseContainerAsync(
                request.Id,
                cancellationToken);
            return true;
        }
    }
}
