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
    public class StartContainerExecCommand : IRequest<MultiplexedStream>
    {
        public string Endpoint { get; set; }

        public string ExecId { get; set; }

        public bool Tty { get; set; }
    }

    public class StartContainerExecCommandHandler : IRequestHandler<StartContainerExecCommand, MultiplexedStream>
    {
        private readonly IDockerClient _client;

        public StartContainerExecCommandHandler(IDockerClient client)
        {
            _client = client;
        }
        public async Task<MultiplexedStream> Handle(StartContainerExecCommand request, CancellationToken cancellationToken)
        {
            return await _client.Exec.StartAndAttachContainerExecAsync(
                request.ExecId,
                request.Tty,
                cancellationToken);
        }
    }
}
