using AutoMapper;
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
    public class ResizeContainerExecTtyCommand : IRequest
    {
        public string Endpoint { get; set; }

        public string ExecId { get; set; }

        public long? Height { get; set; }

        public long? Width { get; set; }
    }

    public class ResizeContainerExecTtyCommandHandler : IRequestHandler<ResizeContainerExecTtyCommand, Unit>
    {
        private readonly IDockerClient _client;

        private readonly IMapper _mapper;

        public ResizeContainerExecTtyCommandHandler(IDockerClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(ResizeContainerExecTtyCommand request, CancellationToken cancellationToken)
        {
            await _client.Exec.ResizeContainerExecTtyAsync(
                request.ExecId,
                _mapper.Map<ContainerResizeParameters>(request),
                cancellationToken);

            return Unit.Value;
        }
    }
}