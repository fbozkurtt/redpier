using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Redpier.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Exec
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class CreateExecCommand : IRequest<string>
    {
        public string Endpoint { get; set; }

        public string ContainerId { get; set; }

        public ContainerExecCreateParameters Parameters { get; set; }
    }

    public class CreateExecCommandHandler : IRequestHandler<CreateExecCommand, string>
    {
        private readonly IDockerClient _client;

        public CreateExecCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<string> Handle(CreateExecCommand request, CancellationToken cancellationToken)
        {
            var result = await _client.Exec.ExecCreateContainerAsync(
                request.ContainerId,
                request.Parameters,
                cancellationToken);

            return result.ID;
        }
    }
}
