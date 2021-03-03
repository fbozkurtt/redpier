using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Swarm
{
    public class InitSwarmCommand : IRequest<string>
    {
        public SwarmInitParameters Parameters { get; set; }
    }

    public class InitSwarmCommandHandler : IRequestHandler<InitSwarmCommand, string>
    {
        private readonly IDockerClient _client;

        public InitSwarmCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<string> Handle(InitSwarmCommand request, CancellationToken cancellationToken)
        {
            return await _client.Swarm.InitSwarmAsync(
                request.Parameters,
                cancellationToken);
        }
    }
}
