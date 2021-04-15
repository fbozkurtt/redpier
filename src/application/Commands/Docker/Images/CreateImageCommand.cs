using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Redpier.Shared.Constants;
using Serilog;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Images
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class CreateImageCommand : IRequest
    {
        public string Endpoint { get; set; }

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
            var Progress = new Progress<JSONMessage>((report) => {
                Log.Information($"{report.Status}");
            });

            await _client.Images.CreateImageAsync(
                request.Parameters,
                request.AuthConfig,
                Progress,
                cancellationToken);

            return Unit.Value;
        }
    }
}
