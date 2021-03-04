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
    public class GetImageHistoryQuery : IRequest<IList<ImageHistoryResponse>>
    {
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
