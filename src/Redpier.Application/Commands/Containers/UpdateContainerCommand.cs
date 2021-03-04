using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Containers
{
    public class UpdateContainerCommand : IRequest<ContainerUpdateResponse>
    {
        public string Id { get; set; }
        public ContainerUpdateParameters Parameters { get; set; }
    }

    public class UpdateContainerCommandHandler : IRequestHandler<UpdateContainerCommand, ContainerUpdateResponse>
    {
        private readonly IDockerClient _client;

        public UpdateContainerCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<ContainerUpdateResponse> Handle(UpdateContainerCommand request, CancellationToken cancellationToken)
        {
            return await _client.Containers.UpdateContainerAsync(
                request.Id,
                request.Parameters,
                cancellationToken);
        }
    }
}
