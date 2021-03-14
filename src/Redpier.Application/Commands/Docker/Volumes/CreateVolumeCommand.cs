using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Redpier.Shared.Constants;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Volumes
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class CreateVolumeCommand : IRequest<VolumeResponse>
    {
        public VolumesCreateParameters Parameters { get; set; }
    }

    public class CreateVolumeCommandHandler : IRequestHandler<CreateVolumeCommand, VolumeResponse>
    {
        private readonly IDockerClient _client;

        public CreateVolumeCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<VolumeResponse> Handle(CreateVolumeCommand request, CancellationToken cancellationToken)
        {
            return await _client.Volumes.CreateAsync(
                request.Parameters,
                cancellationToken);
        }
    }
}
