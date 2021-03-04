using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Containers
{
    public class GetContainerLogsQuery : IRequest<Unit>
    {
        public string Id { get; set; }
        public IProgress<string> Progress { get; set; }
        public ContainerLogsParameters Parameters { get; set; }
    }
    public class GetContainerLogsQueryHandler : IRequestHandler<GetContainerLogsQuery, Unit>
    {
        private readonly IDockerClient _client;

        public GetContainerLogsQueryHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(GetContainerLogsQuery request, CancellationToken cancellationToken)
        {

            await _client.Containers.GetContainerLogsAsync(
                request.Id,
                request.Parameters,
                cancellationToken,
                request.Progress);

            return new Unit();
        }
    }
}
