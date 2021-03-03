using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Images
{
    public class TagImageCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public ImageTagParameters Parameters { get; set; }
    }

    public class TagImageCommandHandler : IRequestHandler<TagImageCommand, bool>
    {
        private readonly IDockerClient _client;

        public TagImageCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<bool> Handle(TagImageCommand request, CancellationToken cancellationToken)
        {
            await _client.Images.TagImageAsync(
                request.Name,
                request.Parameters,
                cancellationToken);
            return true;
        }
    }
}
