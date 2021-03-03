using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Swarm
{
    public class ListNodesQuery : IRequest<IEnumerable<NodeListResponse>>
    {

    }

    public class ListNodesQueryHandler : IRequestHandler<ListNodesQuery, IEnumerable<NodeListResponse>>
    {
        private readonly IDockerClient _client;

        public ListNodesQueryHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<NodeListResponse>> Handle(ListNodesQuery request, CancellationToken cancellationToken)
        {
            return await _client.Swarm.ListNodesAsync(cancellationToken);
        }
    }
}
