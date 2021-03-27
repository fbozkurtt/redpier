using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Docker.Containers
{
    public class InspectContainersQuery : IRequest<ContainerInspectResponse>
    {
        [Required]
        public Guid Endpoint { get; set; }

        [Required]
        public string Id { get; set; }
    }
    public class InspectContainersQueryHandler : IRequestHandler<InspectContainersQuery, ContainerInspectResponse>
    {
        private readonly IDockerClient _client;

        public InspectContainersQueryHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<ContainerInspectResponse> Handle(InspectContainersQuery request, CancellationToken cancellationToken)
        {

            var response = await _client.Containers.InspectContainerAsync(
                request.Id,
                cancellationToken);

            return response;
        }
    }
}
