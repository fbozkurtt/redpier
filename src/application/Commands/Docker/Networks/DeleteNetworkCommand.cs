using Docker.DotNet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Redpier.Shared.Constants;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Networks
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class DeleteNetworkCommand : IRequest
    {
        [Required]
        public string Endpoint { get; set; }

        [Required]
        public string Id { get; set; }
    }

    public class DeleteNetworkCommandHandler : IRequestHandler<DeleteNetworkCommand>
    {
        private readonly IDockerClient _client;

        public DeleteNetworkCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(DeleteNetworkCommand request, CancellationToken cancellationToken)
        {
            await _client.Networks.DeleteNetworkAsync(
                  request.Id,
                  cancellationToken);

            return Unit.Value;
        }
    }
}
