using Docker.DotNet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Redpier.Shared.Constants;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Containers
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class UnpauseContainerCommand : IRequest
    {
        public string Id { get; set; }
    }
    public class UnpauseContainerCommandHandler : IRequestHandler<UnpauseContainerCommand>
    {
        private readonly IDockerClient _client;

        public UnpauseContainerCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(UnpauseContainerCommand request, CancellationToken cancellationToken)
        {
            await _client.Containers.UnpauseContainerAsync(
                request.Id,
                cancellationToken);

            return Unit.Value;
        }
    }
}
