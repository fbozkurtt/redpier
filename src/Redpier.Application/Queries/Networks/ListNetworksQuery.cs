using AutoMapper;
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
    public class ListNetworksQuery : IRequest<IList<NetworkResponse>>
    {
        public NetworksListParameters Parameters { get; set; }
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
                request.Parameters,
                cancellationToken);
        }
    }
}
