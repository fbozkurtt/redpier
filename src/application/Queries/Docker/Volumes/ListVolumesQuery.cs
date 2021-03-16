using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Docker.Volumes
{
    public class ListVolumesQuery : IRequest<VolumesListResponse>
    {
        [Required]
        public string Endpoint { get; set; }
    }

    public class ListVolumesQueryHandler : IRequestHandler<ListVolumesQuery, VolumesListResponse>
    {
        private readonly IDockerClient _client;

        public ListVolumesQueryHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<VolumesListResponse> Handle(ListVolumesQuery request, CancellationToken cancellationToken)
        {
            return await _client.Volumes.ListAsync(cancellationToken);
        }
    }
}
