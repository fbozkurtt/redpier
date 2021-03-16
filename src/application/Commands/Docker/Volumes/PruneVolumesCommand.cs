using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Redpier.Shared.Constants;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Volumes
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class PruneVolumesCommand : IRequest<VolumesPruneResponse>
    {
        [Required]
        public string Endpoint { get; set; }

        //public VolumesPruneParameters Parameters { get; set; }
    }

    public class PruneVolumesCommandHandler : IRequestHandler<PruneVolumesCommand, VolumesPruneResponse>
    {
        private readonly IDockerClient _client;

        public PruneVolumesCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<VolumesPruneResponse> Handle(PruneVolumesCommand request, CancellationToken cancellationToken)
        {
            return await _client.Volumes.PruneAsync(
                null,
                cancellationToken);
        }
    }
}
