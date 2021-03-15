using Docker.DotNet;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Docker.System
{
    public class PingQuery : IRequest
    {

    }

    public class PingQueryHandler : IRequestHandler<PingQuery>
    {
        private readonly IDockerClient _client;

        public PingQueryHandler(IDockerClient client)
        {
            _client = client;
        }
        public async Task<Unit> Handle(PingQuery request, CancellationToken cancellationToken)
        {
            await _client.System.PingAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
