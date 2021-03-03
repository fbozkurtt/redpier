using AutoMapper;
using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Containers
{
    public class InspectContainersQuery : IRequest<ContainerInspectResponse>
    {
        public string ContainerId { get; set; }
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

            var response = await _client.Containers.InspectContainerAsync(request.ContainerId, cancellationToken);

            return response;
        }
    }
}
