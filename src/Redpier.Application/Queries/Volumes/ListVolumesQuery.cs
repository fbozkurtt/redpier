using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Volumes
{
    public class ListVolumesQuery : IRequest<VolumesListResponse>
    {

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
