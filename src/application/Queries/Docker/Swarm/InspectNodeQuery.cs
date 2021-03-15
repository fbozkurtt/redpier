using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Docker.Swarm
{
    public class InspectNodeQuery : IRequest<NodeListResponse>
    {
        public string Id { get; set; }
    }

    public class InspectNodeQueryHandler : IRequestHandler<InspectNodeQuery, NodeListResponse>
    {
        private readonly IDockerClient _client;

        public InspectNodeQueryHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<NodeListResponse> Handle(InspectNodeQuery request, CancellationToken cancellationToken)
        {
            return await _client.Swarm.InspectNodeAsync(
                request.Id,
                cancellationToken);
        }
    }
}
