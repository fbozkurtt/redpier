using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Redpier.Shared.Constants;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Containers
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class RestartContainerCommand : IRequest
    {
        public string Id { get; set; }
        public uint? WaitBeforeKillSeconds { get; set; }
    }
    public class RestartContainerCommandHandler : IRequestHandler<RestartContainerCommand>
    {
        private readonly IDockerClient _client;

        public RestartContainerCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(RestartContainerCommand request, CancellationToken cancellationToken)
        {
            await _client.Containers.RestartContainerAsync(
                request.Id,
                new ContainerRestartParameters() { WaitBeforeKillSeconds = request.WaitBeforeKillSeconds },
                cancellationToken);

            return Unit.Value;
        }
    }
}
