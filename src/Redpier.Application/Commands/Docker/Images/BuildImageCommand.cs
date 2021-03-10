using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Images
{
    public class BuildImageCommand : IRequest
    {
        public MemoryStream DockerfileContent { get; set; }
        public ImageBuildParameters Parameters { get; set; }
    }

    public class BuildImageCommandHandler : IRequestHandler<BuildImageCommand>
    {
        private readonly IDockerClient _client;

        public BuildImageCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(BuildImageCommand request, CancellationToken cancellationToken)
        {
            await _client.Images.BuildImageFromDockerfileAsync(
                request.DockerfileContent,
                request.Parameters,
                cancellationToken);
            return Unit.Value;
        }
    }
}
