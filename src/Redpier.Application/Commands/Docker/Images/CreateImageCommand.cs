using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Images
{
    public class CreateImageCommand : IRequest
    {
        public ImagesCreateParameters Parameters { get; set; }
        public AuthConfig AuthConfig { get; set; }
    }

    public class CreateImageCommandHandler : IRequestHandler<CreateImageCommand>
    {
        private readonly IDockerClient _client;

        public CreateImageCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(CreateImageCommand request, CancellationToken cancellationToken)
        {
            await _client.Images.CreateImageAsync(
                request.Parameters,
                request.AuthConfig,
                new Progress<JSONMessage>(),
                cancellationToken);

            return Unit.Value;
        }
    }
}
