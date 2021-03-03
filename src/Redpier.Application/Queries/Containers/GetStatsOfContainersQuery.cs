using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Containers
{
    public class GetStatsOfContainersQuery : IRequest<Stream>
    {
        public string ContainerId { get; set; }
    }
    public class GetStatsOfContainersQueryHandler : IRequestHandler<GetStatsOfContainersQuery, Stream>
    {
        private readonly IDockerClient _client;

        public GetStatsOfContainersQueryHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Stream> Handle(GetStatsOfContainersQuery request, CancellationToken cancellationToken)
        {
            var response = new MemoryStream();
            //var progress = new Progress<ContainerStatsResponse>((stats) =>
            //{
            //    await response.Write(stats, 0, stats.)
            //});

            //await _client.Containers.GetContainerStatsAsync(
            //    request.ContainerId,
            //    new ContainerStatsParameters(),
            //    progress,
            //    cancellationToken);

            return response;
        }
    }
}
