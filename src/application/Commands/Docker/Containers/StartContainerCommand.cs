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
    public class StartContainerCommand : IRequest
    {
        [Required]
        public string Endpoint { get; set; }

        [Required]
        public string Id { get; set; }

        public string DetachKeys { get; set; }

    }
    public class StartContainerCommandHandler : IRequestHandler<StartContainerCommand>
    {
        private readonly IDockerClient _client;

        public StartContainerCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(StartContainerCommand request, CancellationToken cancellationToken)
        {
            await _client.Containers.StartContainerAsync(
                request.Id,
                new ContainerStartParameters() { DetachKeys = request.DetachKeys },
                cancellationToken);

            return Unit.Value;
        }
    }
}
