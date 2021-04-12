using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Redpier.Shared.Constants;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Networks
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class DisconnectNetworkCommand : IRequest
    {
        public string Endpoint { get; set; }

        [Required]
        public string Id { get; set; }

        public string ContainerId { get; set; }

        public bool Force { get; set; } = false;
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
                  new NetworkDisconnectParameters() { Container = request.ContainerId, Force = request.Force },
                  cancellationToken);

            return Unit.Value;
        }
    }
}
