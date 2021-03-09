using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Images
{
    public class InspectImageQuery : IRequest<ImageInspectResponse>
    {
        public string Name { get; set; }
    }

    public class InspectImageQueryHandler : IRequestHandler<InspectImageQuery, ImageInspectResponse>
    {
        private readonly IDockerClient _client;

        public InspectImageQueryHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<ImageInspectResponse> Handle(InspectImageQuery request, CancellationToken cancellationToken)
        {
            return await _client.Images.InspectImageAsync(
                request.Name,
                cancellationToken);
        }
    }
}
