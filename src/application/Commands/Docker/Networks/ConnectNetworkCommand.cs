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
    public class ConnectNetworkCommand : IRequest
    {
        public string Endpoint { get; set; }

        [Required]
        public string Id { get; set; }

        public NetworkConnectParameters Parameters { get; set; }
    }

    public class ConnectNetworkCommandHandler : IRequestHandler<ConnectNetworkCommand>
    {
        private readonly IDockerClient _client;

        public ConnectNetworkCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(ConnectNetworkCommand request, CancellationToken cancellationToken)
        {
            await _client.Networks.ConnectNetworkAsync(
                  request.Id,
                  request.Parameters,
                  cancellationToken);

            return Unit.Value;
        }
    }
}
