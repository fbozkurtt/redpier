using Docker.DotNet;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Swarm
{
    public class RemoveNodeCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public bool Force { get; set; }
    }
    public class RemoveNodeCommandHandler : IRequestHandler<RemoveNodeCommand, bool>
    {
        private readonly IDockerClient _client;

        public RemoveNodeCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<bool> Handle(RemoveNodeCommand request, CancellationToken cancellationToken)
        {
            await _client.Swarm.RemoveNodeAsync(
                request.Id,
                request.Force,
                cancellationToken);

            return true;
        }
    }
}
