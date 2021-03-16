using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Redpier.Shared.Constants;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Networks
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class PruneNetworksCommand : IRequest<NetworksPruneResponse>
    {
        [Required]
        public string Endpoint { get; set; }
    }

    public class PruneNetworksCommandHandler : IRequestHandler<PruneNetworksCommand, NetworksPruneResponse>
    {
        private readonly IDockerClient _client;

        public PruneNetworksCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<NetworksPruneResponse> Handle(PruneNetworksCommand request, CancellationToken cancellationToken)
        {
            return await _client.Networks.PruneNetworksAsync(
                null,
                cancellationToken);
        }
    }
}
