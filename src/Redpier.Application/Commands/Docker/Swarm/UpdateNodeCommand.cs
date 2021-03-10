using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Swarm
{
    public class UpdateNodeCommand : IRequest
    {
        public string Id { get; set; }
        public ulong version { get; set; }
        public NodeUpdateParameters Parameters { get; set; }
    }

    public class UpdateNodeCommandHandler : IRequestHandler<UpdateNodeCommand>
    {
        private readonly IDockerClient _client;

        public UpdateNodeCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(UpdateNodeCommand request, CancellationToken cancellationToken)
        {
            await _client.Swarm.UpdateNodeAsync(
                request.Id,
                request.version,
                request.Parameters,
                cancellationToken);

            return Unit.Value;
        }
    }
}
