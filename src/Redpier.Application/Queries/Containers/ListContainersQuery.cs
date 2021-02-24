using AutoMapper;
using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Containers
{
    public class ListContainersQuery : IRequest<List<ContainerListResponse>>
    {
        public bool ListAll { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class ListContainersQueryHandler : IRequestHandler<ListContainersQuery, List<ContainerListResponse>>
    {
        private readonly IDockerClient _client;
        private readonly IMapper _mapper;

        public ListContainersQueryHandler(IDockerClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<List<ContainerListResponse>> Handle(ListContainersQuery request, CancellationToken cancellationToken)
        {

            var containers = await _client.Containers.ListContainersAsync(new ContainersListParameters()
            {
                All = false
            }, cancellationToken);

            return _mapper.Map<List<ContainerListResponse>>(containers);
        }
    }
}
