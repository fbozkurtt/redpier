using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Docker.Images
{
    public class ListImagesQuery : IRequest<IList<ImagesListResponse>>
    {
        public ImagesListParameters parameters { get; set; }
    }

    public class ListImagesQueryHandler : IRequestHandler<ListImagesQuery, IList<ImagesListResponse>>
    {
        private readonly IDockerClient _client;

        public ListImagesQueryHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<IList<ImagesListResponse>> Handle(ListImagesQuery request, CancellationToken cancellationToken)
        {
            var response = await _client.Images.ListImagesAsync(
                request.parameters,
                cancellationToken);

            return response;
        }
    }
}
