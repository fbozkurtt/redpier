using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Swarm
{
    public class UnlockSwarmCommand : IRequest
    {
        public SwarmUnlockParameters Parameters { get; set; }
    }

    public class UnlockSwarmCommandHandler : IRequestHandler<UnlockSwarmCommand>
    {
        private readonly IDockerClient _client;

        public UnlockSwarmCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(UnlockSwarmCommand request, CancellationToken cancellationToken)
        {
            await _client.Swarm.UnlockSwarmAsync(
                request.Parameters,
                cancellationToken);

            return Unit.Value;
        }
    }
}
