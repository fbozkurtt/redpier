using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Images
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
            return await _client.Images.ListImagesAsync(
                request.parameters,
                cancellationToken);
        }
    }
}
