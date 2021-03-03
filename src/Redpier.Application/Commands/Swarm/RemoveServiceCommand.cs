using Docker.DotNet;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Swarm
{
    public class RemoveServiceCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }
    public class RemoveServiceCommandHandler : IRequestHandler<RemoveServiceCommand, bool>
    {
        private readonly IDockerClient _client;

        public RemoveServiceCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<bool> Handle(RemoveServiceCommand request, CancellationToken cancellationToken)
        {
            await _client.Swarm.RemoveServiceAsync(
                request.Id,
                cancellationToken);

            return true;
        }
    }
}
