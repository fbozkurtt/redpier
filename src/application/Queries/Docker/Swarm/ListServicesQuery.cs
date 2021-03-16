using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Docker.Swarm
{
    public class ListServicesQuery : IRequest<IEnumerable<SwarmService>>
    {
        [Required]
        public string Endpoint { get; set; }

        public ServicesListParameters Parameters { get; set; }
    }

    public class ListServicesQueryHandler : IRequestHandler<ListServicesQuery, IEnumerable<SwarmService>>
    {
        private readonly IDockerClient _client;

        public ListServicesQueryHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<SwarmService>> Handle(ListServicesQuery request, CancellationToken cancellationToken)
        {
            return await _client.Swarm.ListServicesAsync(
                request.Parameters,
                cancellationToken);
        }
    }
}
