using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.Swarm
{
    public class CreateServiceCommand : IRequest<ServiceCreateResponse>
    {
        public ServiceCreateParameters Parameters { get; set; }
    }

    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, ServiceCreateResponse>
    {
        private readonly IDockerClient _client;

        public CreateServiceCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<ServiceCreateResponse> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            return await _client.Swarm.CreateServiceAsync(
                request.Parameters,
                cancellationToken);
        }
    }
}
