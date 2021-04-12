using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Docker.Images
{
    public class ListImagesQuery : IRequest<IList<ImagesListResponse>>
    {
        [Required]
        public string Endpoint { get; set; }

        public string MatchName { get; set; }

        public bool? All { get; set; } = true;
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
                new ImagesListParameters() { All = request.All, MatchName = request.MatchName },
                cancellationToken);

            return response;
        }
    }
}
