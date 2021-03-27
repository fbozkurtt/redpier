using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Docker.Swarm
{
    public class InspectServiceQuery : IRequest<SwarmService>
    {
        [Required]
        public Guid Endpoint { get; set; }

        public string Id { get; set; }
    }

    public class InspectServiceQueryHandler : IRequestHandler<InspectServiceQuery, SwarmService>
    {
        private readonly IDockerClient _client;

        public InspectServiceQueryHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<SwarmService> Handle(InspectServiceQuery request, CancellationToken cancellationToken)
        {
            return await _client.Swarm.InspectServiceAsync(
                request.Id,
                cancellationToken);
        }
    }
}
