using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Docker.Swarm
{
    public class GetSwarmUnlockKeyQuery : IRequest<SwarmUnlockResponse>
    {
        [Required]
        public string Endpoint { get; set; }

    }

    public class GetSwarmUnlockKeyQueryHandler : IRequestHandler<GetSwarmUnlockKeyQuery, SwarmUnlockResponse>
    {
        private readonly IDockerClient _client;

        public GetSwarmUnlockKeyQueryHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<SwarmUnlockResponse> Handle(GetSwarmUnlockKeyQuery request, CancellationToken cancellationToken)
        {
            return await _client.Swarm.GetSwarmUnlockKeyAsync(cancellationToken);
        }
    }
}
