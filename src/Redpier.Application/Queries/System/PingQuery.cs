using Docker.DotNet;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.System
{
    public class PingQuery : IRequest<bool>
    {

    }

    public class PingQueryHandler : IRequestHandler<PingQuery, bool>
    {
        private readonly IDockerClient _client;

        public PingQueryHandler(IDockerClient client)
        {
            _client = client;
        }
        public async Task<bool> Handle(PingQuery request, CancellationToken cancellationToken)
        {
            await _client.System.PingAsync(cancellationToken);

            return true;
        }
    }
}
