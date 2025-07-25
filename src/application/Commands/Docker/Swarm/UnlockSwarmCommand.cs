﻿using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Redpier.Shared.Constants;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Swarm
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class UnlockSwarmCommand : IRequest
    {
        [Required]
        public string Endpoint { get; set; }

        [Required]
        public string UnlockKey { get; set; }
    }

    public class UnlockSwarmCommandHandler : IRequestHandler<UnlockSwarmCommand>
    {
        private readonly IDockerClient _client;

        public UnlockSwarmCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(UnlockSwarmCommand request, CancellationToken cancellationToken)
        {
            await _client.Swarm.UnlockSwarmAsync(
                new SwarmUnlockParameters() { UnlockKey = request.UnlockKey },
                cancellationToken);

            return Unit.Value;
        }
    }
}
