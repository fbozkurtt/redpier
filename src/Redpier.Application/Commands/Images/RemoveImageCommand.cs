using AutoMapper;
using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Images
{
    public class RemoveImageCommand : IRequest<List<Dictionary<string, string>>>
    {
        public string Name { get; set; }
        public ImageDeleteParameters Parameters { get; set; }
    }

    public class RemoveImageCommandHandler : IRequestHandler<RemoveImageCommand, List<Dictionary<string, string>>>
    {
        private readonly IDockerClient _client;
        private readonly IMapper _mapper;

        public RemoveImageCommandHandler(IDockerClient client,, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<List<Dictionary<string, string>>> Handle(RemoveImageCommand request, CancellationToken cancellationToken)
        {
            var response = await _client.Images.DeleteImageAsync(
                request.Name,
                request.Parameters,
                cancellationToken);

            return _mapper.Map<List<Dictionary<string, string>>>(response);
        }
    }
}
