using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Docker.System
{
    public class GetSystemInfoQuery : IRequest<SystemInfoResponse>
    {
        [Required]
        public string Endpoint { get; set; }

    }

    public class GetSystemInfoQueryHandler : IRequestHandler<GetSystemInfoQuery, SystemInfoResponse>
    {
        private readonly IDockerClient _client;

        public GetSystemInfoQueryHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<SystemInfoResponse> Handle(GetSystemInfoQuery request, CancellationToken cancellationToken)
        {
            return await _client.System.GetSystemInfoAsync(cancellationToken);
        }
    }
}
