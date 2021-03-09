using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Networks
{
    public class ConnectNetworkCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public NetworkConnectParameters Parameters { get; set; }
    }

    public class ConnectNetworkCommandHandler : IRequestHandler<ConnectNetworkCommand, bool>
    {
        private readonly IDockerClient _client;

        public ConnectNetworkCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<bool> Handle(ConnectNetworkCommand request, CancellationToken cancellationToken)
        {
            await _client.Networks.ConnectNetworkAsync(
                  request.Id,
                  request.Parameters,
                  cancellationToken);

            return true;
        }
    }
}
