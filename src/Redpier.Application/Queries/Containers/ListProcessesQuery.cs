using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Containers
{
    public class ListProcessesQuery : IRequest<ContainerProcessesResponse>
    {
        public string Id { get; set; }
        public ContainerListProcessesParameters Parameters { get; set; }
    }

    public class ListProcessesQueryHandler : IRequestHandler<ListProcessesQuery, ContainerProcessesResponse>
    {
        private readonly IDockerClient _client;

        public ListProcessesQueryHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<ContainerProcessesResponse> Handle(ListProcessesQuery request, CancellationToken cancellationToken)
        {
            return await _client.Containers.ListProcessesAsync(
                request.Id,
                request.Parameters,
                cancellationToken
                );
        }
    }
}
