using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Networks
{
    public class DisconnectNetworkCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public NetworkDisconnectParameters Parameters { get; set; }
    }

    public class DisconnectNetworkCommandHandler : IRequestHandler<DisconnectNetworkCommand, bool>
    {
        private readonly IDockerClient _client;

        public DisconnectNetworkCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<bool> Handle(DisconnectNetworkCommand request, CancellationToken cancellationToken)
        {
            await _client.Networks.DisconnectNetworkAsync(
                  request.Id,
                  request.Parameters,
                  cancellationToken);

            return true;
        }
    }
}
