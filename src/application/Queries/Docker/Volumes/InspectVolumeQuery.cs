using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Docker.Volumes
{
    public class InspectVolumeQuery : IRequest<VolumeResponse>
    {
        [Required]
        public string Endpoint { get; set; }

        public string Name { get; set; }
    }

    public class InspectVolumeQueryHandler : IRequestHandler<InspectVolumeQuery, VolumeResponse>
    {
        private readonly IDockerClient _client;

        public InspectVolumeQueryHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<VolumeResponse> Handle(InspectVolumeQuery request, CancellationToken cancellationToken)
        {
            return await _client.Volumes.InspectAsync(
                request.Name,
                cancellationToken);
        }
    }
}
