using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Redpier.Shared.Constants;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Networks
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class DisconnectNetworkCommand : IRequest
    {
        public string Id { get; set; }
        public NetworkDisconnectParameters Parameters { get; set; }
    }

    public class DisconnectNetworkCommandHandler : IRequestHandler<DisconnectNetworkCommand>
    {
        private readonly IDockerClient _client;

        public DisconnectNetworkCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(DisconnectNetworkCommand request, CancellationToken cancellationToken)
        {
            await _client.Networks.DisconnectNetworkAsync(
                  request.Id,
                  request.Parameters,
                  cancellationToken);

            return Unit.Value;
        }
    }
}
