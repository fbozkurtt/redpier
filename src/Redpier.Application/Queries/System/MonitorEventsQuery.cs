using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.System
{
    public class MonitorEventsQuery : IRequest<Unit>
    {
        public ContainerEventsParameters Parameters { get; set; }
        public IProgress<Message> Progress { get; set; }
    }

    public class MonitorEventsQueryHandler : IRequestHandler<MonitorEventsQuery, Unit>
    {
        private readonly IDockerClient _client;

        public MonitorEventsQueryHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(MonitorEventsQuery request, CancellationToken cancellationToken)
        {
            await _client.System.MonitorEventsAsync(
                request.Parameters,
                request.Progress,
                cancellationToken);

            return new Unit();
        }
    }
}
