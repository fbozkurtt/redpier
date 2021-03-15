using AutoMapper;
using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using Redpier.Application.Common.Mappings;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Docker.Containers
{
    public class ListContainersQuery : IRequest<IList<ContainerListResponse>>, IMapFrom<ContainersListParameters>
    {
        public bool? Size { get; set; }

        public bool? All { get; set; } = false;

        public string Since { get; set; }

        public string Before { get; set; }

        public long? Limit { get; set; }

        public IDictionary<string, IDictionary<string, bool>> Filters { get; set; }
    }

    public class ListContainersQueryHandler : IRequestHandler<ListContainersQuery, IList<ContainerListResponse>>
    {
        private readonly IDockerClient _client;
        private readonly IMapper _mapper;

        public ListContainersQueryHandler(IDockerClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<IList<ContainerListResponse>> Handle(ListContainersQuery request, CancellationToken cancellationToken)
        {
            return await _client.Containers.ListContainersAsync(
                _mapper.Map<ContainersListParameters>(request),
                cancellationToken);
        }
    }
}
