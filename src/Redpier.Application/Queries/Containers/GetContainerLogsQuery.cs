using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Containers
{
    public class GetContainerLogsQuery : IRequest<MultiplexedStream>
    {
        public string Id { get; set; }
        public bool Tty { get; set; }
        public ContainerLogsParameters Parameters { get; set; }
    }
    public class GetContainerLogsQueryHandler : IRequestHandler<GetContainerLogsQuery, MultiplexedStream>
    {
        private readonly IDockerClient _client;

        public GetContainerLogsQueryHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<MultiplexedStream> Handle(GetContainerLogsQuery request, CancellationToken cancellationToken)
        {

            var response = await _client.Containers.GetContainerLogsAsync(
                request.Id,
                request.Tty,
                request.Parameters,
                cancellationToken);

            return response;
        }
    }
}
