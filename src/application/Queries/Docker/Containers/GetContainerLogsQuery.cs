using AutoMapper;
using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using Redpier.Application.Common.Mappings;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Docker.Containers
{
    public class GetContainerLogsQuery : IRequest<Stream>, IMapFrom<ContainerLogsParameters>
    {
        public string Endpoint { get; set; }

        public string Id { get; set; }

        public bool? ShowStdout { get; set; }

        public bool? ShowStderr { get; set; }

        public string Since { get; set; }

        public bool? Timestamps { get; set; }

        public bool? Follow { get; set; }

        public string Tail { get; set; }
    }

    public class GetContainerLogsQueryHandler : IRequestHandler<GetContainerLogsQuery, Stream>
    {
        private readonly IDockerClient _client;
        private readonly IMapper _mapper;

        public GetContainerLogsQueryHandler(IDockerClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<Stream> Handle(GetContainerLogsQuery request, CancellationToken cancellationToken)
        {

            return await _client.Containers.GetContainerLogsAsync(
                    request.Id,
                    _mapper.Map<ContainerLogsParameters>(request),
                    cancellationToken);
        }
    }
}
