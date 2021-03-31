using AutoMapper;
using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Redpier.Shared.Constants;
using Redpier.Shared.Mappings;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Containers
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class RemoveContainerCommand : IRequest, IMapFrom<ContainerRemoveParameters>
    {
        [Required]
        public string Endpoint { get; set; }

        [Required]
        public string Id { get; set; }

        public bool? RemoveVolumes { get; set; }

        public bool? RemoveLinks { get; set; }

        public bool? Force { get; set; }
    }

    public class RemoveContainerCommandHandler : IRequestHandler<RemoveContainerCommand>
    {
        private readonly IDockerClient _client;
        private readonly IMapper _mapper;

        public RemoveContainerCommandHandler(IDockerClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(RemoveContainerCommand request, CancellationToken cancellationToken)
        {
            await _client.Containers.RemoveContainerAsync(request.Id,
                _mapper.Map<ContainerRemoveParameters>(request),
                cancellationToken);

            return Unit.Value;
        }
    }
}
