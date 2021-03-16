using Docker.DotNet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Redpier.Shared.Constants;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Containers
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class KillContainerCommand : IRequest
    {
        [Required]
        public string Endpoint { get; set; }

        [Required]
        public string Id { get; set; }
    }

    public class KillContainersCommandHandler : IRequestHandler<KillContainerCommand>
    {
        private readonly IDockerClient _client;

        public KillContainersCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(KillContainerCommand request, CancellationToken cancellationToken)
        {
            await _client.Containers.KillContainerAsync(
                request.Id,
                null,
                cancellationToken);

            return Unit.Value;
        }
    }
}
