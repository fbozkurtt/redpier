using Docker.DotNet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Redpier.Shared.Constants;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Swarm
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class RemoveServiceCommand : IRequest
    {
        public string Id { get; set; }
    }
    public class RemoveServiceCommandHandler : IRequestHandler<RemoveServiceCommand>
    {
        private readonly IDockerClient _client;

        public RemoveServiceCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(RemoveServiceCommand request, CancellationToken cancellationToken)
        {
            await _client.Swarm.RemoveServiceAsync(
                request.Id,
                cancellationToken);

            return Unit.Value;
        }
    }
}
