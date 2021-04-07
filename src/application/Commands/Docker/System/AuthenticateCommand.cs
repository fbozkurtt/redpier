using AutoMapper;
using Docker.DotNet;
using Docker.DotNet.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Redpier.Application.Common.Mappings;
using Redpier.Shared.Constants;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.System
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class AuthenticateCommand : IRequest, IMapFrom<AuthConfig>
    {
        [Required]
        public string Endpoint { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Auth { get; set; }

        public string Email { get; set; }

        public string ServerAddress { get; set; }

        public string IdentityToken { get; set; }

        public string RegistryToken { get; set; }
    }

    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand>
    {
        private readonly IDockerClient _client;
        private readonly IMapper _mapper;

        public AuthenticateCommandHandler(IDockerClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            await _client.System.AuthenticateAsync(
                _mapper.Map<AuthConfig>(request),
                cancellationToken);

            return Unit.Value;
        }
    }
}
