using Docker.DotNet;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Swarm
{
    public class RemoveNodeCommand : IRequest
    {
        public string Id { get; set; }
        public bool Force { get; set; }
    }
    public class RemoveNodeCommandHandler : IRequestHandler<RemoveNodeCommand>
    {
        private readonly IDockerClient _client;

        public RemoveNodeCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(RemoveNodeCommand request, CancellationToken cancellationToken)
        {
            await _client.Swarm.RemoveNodeAsync(
                request.Id,
                request.Force,
                cancellationToken);

            return Unit.Value;
        }
    }
}
