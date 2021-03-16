using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Redpier.Shared.Constants;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Containers
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class RenameContainerCommand : IRequest
    {
        [Required]
        public string Endpoint { get; set; }

        [Required]
        public string Id { get; set; }

        [Required]
        public string NewName { get; set; }
    }

    public class RenameContainerCommandQuery : IRequestHandler<RenameContainerCommand>
    {
        private readonly IDockerClient _client;

        public RenameContainerCommandQuery(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(RenameContainerCommand request, CancellationToken cancellationToken)
        {
            await _client.Containers.RenameContainerAsync(
                request.Id,
                new ContainerRenameParameters() { NewName = request.NewName },
                cancellationToken);

            return Unit.Value;
        }
    }
}
