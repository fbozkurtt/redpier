using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Docker.Swarm
{
    public class InspectSwarmQuery : IRequest<SwarmInspectResponse>
    {
        [Required]
        public string Endpoint { get; set; }
    }

    public class InspectSwarmQueryHandler : IRequestHandler<InspectSwarmQuery, SwarmInspectResponse>
    {
        private readonly IDockerClient _client;

        public InspectSwarmQueryHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<SwarmInspectResponse> Handle(InspectSwarmQuery request, CancellationToken cancellationToken)
        {
            return await _client.Swarm.InspectSwarmAsync(cancellationToken);
        }
    }
}
