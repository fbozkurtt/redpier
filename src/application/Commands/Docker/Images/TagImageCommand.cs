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

namespace Redpier.Application.Commands.Docker.Images
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class TagImageCommand : IRequest, IMapFrom<ImageTagParameters>
    {
        [Required]
        public string Endpoint { get; set; }

        [Required]
        public string Name { get; set; }

        public string RepositoryName { get; set; }

        [Required]
        public string Tag { get; set; }

        public bool? Force { get; set; }
    }

    public class TagImageCommandHandler : IRequestHandler<TagImageCommand>
    {
        private readonly IDockerClient _client;
        private readonly IMapper _mapper;

        public TagImageCommandHandler(IDockerClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(TagImageCommand request, CancellationToken cancellationToken)
        {
            await _client.Images.TagImageAsync(
                request.Name,
                _mapper.Map<ImageTagParameters>(request),
                cancellationToken);

            return Unit.Value;
        }
    }
}
