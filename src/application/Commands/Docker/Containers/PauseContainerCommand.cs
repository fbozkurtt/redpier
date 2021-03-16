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
    public class PauseContainerCommand : IRequest
    {
        [Required]
        public string Endpoint { get; set; }

        [Required]
        public string Id { get; set; }
    }

    public class PauseContainerCommandHandler : IRequestHandler<PauseContainerCommand>
    {
        private readonly IDockerClient _client;

        public PauseContainerCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(PauseContainerCommand request, CancellationToken cancellationToken)
        {
            await _client.Containers.PauseContainerAsync(
                request.Id,
                cancellationToken);

            return Unit.Value;
        }
    }
}
