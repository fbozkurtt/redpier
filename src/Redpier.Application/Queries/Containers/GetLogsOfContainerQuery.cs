using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Containers
{
    public class GetLogsOfContainerQuery : IRequest<MultiplexedStream>
    {
        public string ContainerId { get; set; }
    }
    public class GetLogsOfContainerQueryHandler : IRequestHandler<GetLogsOfContainerQuery, MultiplexedStream>
    {
        private readonly IDockerClient _client;

        public GetLogsOfContainerQueryHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<MultiplexedStream> Handle(GetLogsOfContainerQuery request, CancellationToken cancellationToken)
        {

            var response = await _client.Containers.GetContainerLogsAsync(request.ContainerId, false, new ContainerLogsParameters(), cancellationToken);

            return response;
        }
    }
}
