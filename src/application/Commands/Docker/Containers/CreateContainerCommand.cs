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
    public class CreateContainerCommand : IRequest<CreateContainerResponse>
    {
        public string Endpoint { get; set; }

        public CreateContainerParameters Parameters { get; set; }
    }

    public class CreateContainerCommandHandler : IRequestHandler<CreateContainerCommand, CreateContainerResponse>
    {
        private readonly IDockerClient _client;

        public CreateContainerCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<CreateContainerResponse> Handle(CreateContainerCommand request, CancellationToken cancellationToken)
        {
            return await _client.Containers.CreateContainerAsync(
                request.Parameters,
                cancellationToken);
        }
    }
}
