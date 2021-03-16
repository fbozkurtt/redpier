using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Docker.System
{
    public class GetVersionQuery : IRequest<VersionResponse>
    {
        [Required]
        public string Endpoint { get; set; }
    }

    public class GetVersionQueryHandler : IRequestHandler<GetVersionQuery, VersionResponse>
    {
        private readonly IDockerClient _client;

        public GetVersionQueryHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<VersionResponse> Handle(GetVersionQuery request, CancellationToken cancellationToken)
        {
            return await _client.System.GetVersionAsync(cancellationToken);
        }
    }
}
