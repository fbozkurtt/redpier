using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.System
{
    public class AuthenticateCommand : IRequest
    {
        public AuthConfig Parameters { get; set; }
    }

    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand>
    {
        private readonly IDockerClient _client;

        public AuthenticateCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            await _client.System.AuthenticateAsync(
                request.Parameters,
                cancellationToken);

            return Unit.Value;
        }
    }
}
