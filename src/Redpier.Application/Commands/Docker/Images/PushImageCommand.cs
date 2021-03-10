using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Images
{
    public class PushImageCommand : IRequest
    {
        public string Name { get; set; }
        public AuthConfig AuthConfig { get; set; }
        public ImagePushParameters Parameters { get; set; }
    }

    public class PushImageCommandHandler : IRequestHandler<PushImageCommand>
    {
        private readonly IDockerClient _client;

        public PushImageCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(PushImageCommand request, CancellationToken cancellationToken)
        {
            await _client.Images.PushImageAsync(
                request.Name,
                request.Parameters,
                request.AuthConfig,
                new Progress<JSONMessage>(),
                cancellationToken);

            return Unit.Value;
        }
    }
}
