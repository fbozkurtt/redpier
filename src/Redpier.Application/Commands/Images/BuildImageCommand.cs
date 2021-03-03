using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Images
{
    public class BuildImageCommand : IRequest<bool>
    {
        public MemoryStream DockerfileContent { get; set; }
        public ImageBuildParameters Parameters { get; set; }
    }

    public class BuildImageCommandHandler : IRequestHandler<BuildImageCommand, bool>
    {
        private readonly IDockerClient _client;

        public BuildImageCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<bool> Handle(BuildImageCommand request, CancellationToken cancellationToken)
        {
            await _client.Images.BuildImageFromDockerfileAsync(
                request.DockerfileContent,
                request.Parameters,
                cancellationToken);
            return true;
        }
    }
}
