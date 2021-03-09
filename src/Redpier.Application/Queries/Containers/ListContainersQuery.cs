using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Containers
{
    public class ListContainersQuery : IRequest<IList<ContainerListResponse>>
    {
        public ContainersListParameters Parameters { get; set; }
    }

    public class ListContainersQueryHandler : IRequestHandler<ListContainersQuery, IList<ContainerListResponse>>
    {
        private readonly IDockerClient _client;

        public ListContainersQueryHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<IList<ContainerListResponse>> Handle(ListContainersQuery request, CancellationToken cancellationToken)
        {
            return await _client.Containers.ListContainersAsync(
                request.Parameters ??= new ContainersListParameters(),
                cancellationToken);
        }
    }
}
