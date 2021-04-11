using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Docker.Networks
{
    public class ListNetworksQuery : IRequest<IList<NetworkResponse>>
    {
        public Guid? Endpoint { get; set; }

        public IDictionary<string, IDictionary<string, bool>> Filters { get; set; }
    }

    public class ListNetworksQueryHandler : IRequestHandler<ListNetworksQuery, IList<NetworkResponse>>
    {
        private readonly IDockerClient _client;

        public ListNetworksQueryHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<IList<NetworkResponse>> Handle(ListNetworksQuery request, CancellationToken cancellationToken)
        {
            return await _client.Networks.ListNetworksAsync(
                new NetworksListParameters() { Filters = request.Filters },
                cancellationToken);
        }
    }
}
