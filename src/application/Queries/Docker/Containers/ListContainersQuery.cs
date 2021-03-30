using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Docker.Containers
{
    public class ListContainersQuery : IRequest<IList<ContainerListResponse>>
    {
        [Required]
        public Guid Endpoint { get; set; }

        public bool? All { get; set; } = false;
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
                new ContainersListParameters() { All = request.All },
                cancellationToken);
        }
    }
}
