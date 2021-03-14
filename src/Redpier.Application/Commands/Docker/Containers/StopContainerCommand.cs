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
    public class StopContainerCommand : IRequest
    {
        public string Id { get; set; }

        public uint? WaitBeforeKillSeconds { get; set; }
    }
    public class StopContainerCommandHandler : IRequestHandler<StopContainerCommand>
    {
        private readonly IDockerClient _client;

        public StopContainerCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(StopContainerCommand request, CancellationToken cancellationToken)
        {
            await _client.Containers.StopContainerAsync(
                request.Id,
                new ContainerStopParameters() { WaitBeforeKillSeconds = request.WaitBeforeKillSeconds },
                cancellationToken);

            return Unit.Value;
        }
    }
}
