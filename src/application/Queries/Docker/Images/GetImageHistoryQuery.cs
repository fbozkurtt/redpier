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
    public class GetImageHistoryQuery : IRequest<IList<ImageHistoryResponse>>
    {
        [Required]
        public Guid Endpoint { get; set; }

        public string Name { get; set; }
    }

    public class GetImageHistoryQueryHandler : IRequestHandler<GetImageHistoryQuery, IList<ImageHistoryResponse>>
    {
        private readonly IDockerClient _client;

        public GetImageHistoryQueryHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<IList<ImageHistoryResponse>> Handle(GetImageHistoryQuery request, CancellationToken cancellationToken)
        {
            return await _client.Images.GetImageHistoryAsync(
                request.Name,
                cancellationToken);
        }
    }
}
