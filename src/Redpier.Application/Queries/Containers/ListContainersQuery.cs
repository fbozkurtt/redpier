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
        public ContainersListParameters Parameters { get; set; }
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

            var response = await _client.Containers.ListContainersAsync(request.Parameters, cancellationToken);

            return _mapper.Map<List<ContainerListResponse>>(response);
        }
    }
}
