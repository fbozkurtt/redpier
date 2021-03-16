using Docker.DotNet;
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
    public class InitSwarmCommand : IRequest<string>
    {
        [Required]
        public string Endpoint { get; set; }


        public SwarmInitParameters Parameters { get; set; }
    }

    public class InitSwarmCommandHandler : IRequestHandler<InitSwarmCommand, string>
    {
        private readonly IDockerClient _client;

        public InitSwarmCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<string> Handle(InitSwarmCommand request, CancellationToken cancellationToken)
        {
            return await _client.Swarm.InitSwarmAsync(
                request.Parameters,
                cancellationToken);
        }
    }
}
