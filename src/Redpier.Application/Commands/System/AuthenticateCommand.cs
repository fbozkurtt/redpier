using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.System
{
    public class AuthenticateCommand : IRequest<bool>
    {
        public AuthConfig Parameters { get; set; }
    }

    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, bool>
    {
        private readonly IDockerClient _client;

        public AuthenticateCommandHandler(IDockerClient client)
        {
            _client = client;
        }

        public async Task<bool> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            await _client.System.AuthenticateAsync(
                request.Parameters,
                cancellationToken);

            return true;
        }
    }
}
