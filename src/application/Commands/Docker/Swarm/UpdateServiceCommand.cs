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
    public class UpdateServiceCommand : IRequest<ServiceUpdateResponse>
    {
        [Required]
        public string Endpoint { get; set; }

        [Required]
        public string Id { get; set; }

        public ServiceUpdateParameters Parameters { get; set; }
    }
    public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, ServiceUpdateResponse>
    {
        private readonly IDockerClient _client;

        public UpdateServiceCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<ServiceUpdateResponse> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            return await _client.Swarm.UpdateServiceAsync(
                request.Id,
                request.Parameters,
                cancellationToken);
        }
    }
}
