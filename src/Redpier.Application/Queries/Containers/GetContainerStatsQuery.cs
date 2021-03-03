using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Containers
{
    public class GetContainerStatsQuery : IRequest<Unit>
    {
        public string Id { get; set; }
        public ContainerStatsParameters Parameters { get; set; }
        public IProgress<ContainerStatsResponse> Progress { get; set; }
    }
    public class GetContainerStatsQueryHandler : IRequestHandler<GetContainerStatsQuery, Unit>
    {
        private readonly IDockerClient _client;

        public GetContainerStatsQueryHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(GetContainerStatsQuery request, CancellationToken cancellationToken)
        {
            await _client.Containers.GetContainerStatsAsync(
                request.Id,
                request.Parameters,
                request.Progress,
                cancellationToken);

            return new Unit();
        }
    }
}
