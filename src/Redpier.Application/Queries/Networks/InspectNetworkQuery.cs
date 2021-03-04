using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Networks
{
    public class InspectNetworkQuery : IRequest<NetworkResponse>
    {
        public string Id { get; set; }
    }

    public class InspectNetworkQueryHandler : IRequestHandler <InspectNetworkQuery, NetworkResponse>
    {
        private readonly IDockerClient _client;

        public InspectNetworkQueryHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<NetworkResponse> Handle(InspectNetworkQuery request, CancellationToken cancellationToken)
        {
            return await _client.Networks.InspectNetworkAsync(
                request.Id,
                cancellationToken);
        }
    }
}
