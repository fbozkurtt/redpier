using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Docker.Images
{
    public class SearchImagesQuery : IRequest<IList<ImageSearchResponse>>
    {
        public ImagesSearchParameters Parameters { get; set; }
    }

    public class SearchImagesQueryHandler : IRequestHandler<SearchImagesQuery, IList<ImageSearchResponse>>
    {
        private readonly IDockerClient _client;

        public SearchImagesQueryHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<IList<ImageSearchResponse>> Handle(SearchImagesQuery request, CancellationToken cancellationToken)
        {
            return await _client.Images.SearchImagesAsync(
                request.Parameters,
                cancellationToken);
        }
    }
}
