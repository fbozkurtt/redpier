using AutoMapper;
using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Redpier.Shared.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Images
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class RemoveImageCommand : IRequest<IList<IDictionary<string, string>>>
    {
        [Required]
        public string Endpoint { get; set; }

        [Required]
        public string Name { get; set; }

        public ImageDeleteParameters Parameters { get; set; }
    }

    public class RemoveImageCommandHandler : IRequestHandler<RemoveImageCommand, IList<IDictionary<string, string>>>
    {
        private readonly IDockerClient _client;
        private readonly IMapper _mapper;

        public RemoveImageCommandHandler(IDockerClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<IList<IDictionary<string, string>>> Handle(RemoveImageCommand request, CancellationToken cancellationToken)
        {
            return await _client.Images.DeleteImageAsync(
                request.Name,
                request.Parameters,
                cancellationToken);
        }
    }
}
