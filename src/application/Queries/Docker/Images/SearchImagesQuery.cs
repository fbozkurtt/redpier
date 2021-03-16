using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Docker.Images
{
    public class SearchImagesQuery : IRequest<IList<ImageSearchResponse>>
    {
        [Required]
        public string Endpoint { get; set; }

        public string Term { get; set; }

        public AuthConfig RegistryAuth { get; set; }
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
                new ImagesSearchParameters() { Term = request.Term, RegistryAuth = request.RegistryAuth },
                cancellationToken);
        }
    }
}
