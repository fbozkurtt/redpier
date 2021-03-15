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
    public class CreateNetworkCommand : IRequest<NetworksCreateResponse>
    {
        public NetworksCreateParameters Parameters { get; set; }
    }

    public class CreateNetworkCommandHandler : IRequestHandler<CreateNetworkCommand, NetworksCreateResponse>
    {
        private readonly IDockerClient _client;

        public CreateNetworkCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<NetworksCreateResponse> Handle(CreateNetworkCommand request, CancellationToken cancellationToken)
        {
            return await _client.Networks.CreateNetworkAsync(
                request.Parameters,
                cancellationToken);
        }
    }
}
