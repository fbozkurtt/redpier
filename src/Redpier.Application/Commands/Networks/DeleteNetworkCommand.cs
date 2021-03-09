using Docker.DotNet;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Networks
{
    public class DeleteNetworkCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }

    public class DeleteNetworkCommandHandler : IRequestHandler<DeleteNetworkCommand, bool>
    {
        private readonly IDockerClient _client;

        public DeleteNetworkCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<bool> Handle(DeleteNetworkCommand request, CancellationToken cancellationToken)
        {
            await _client.Networks.DeleteNetworkAsync(
                  request.Id,
                  cancellationToken);

            return true;
        }
    }
}
