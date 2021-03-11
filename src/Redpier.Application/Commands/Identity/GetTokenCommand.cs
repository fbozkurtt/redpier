using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Redpier.Application.Common.Interfaces.Identity;
using Redpier.Application.Common.Interfaces.Repositories;
using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Identity
{
    public class GetTokenCommand : IRequest<string>
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class GetTokenCommandHandler : IRequestHandler<GetTokenCommand, string>
    {
        private readonly IIdentityService _identityService;

        public GetTokenCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<string> Handle(GetTokenCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.GetTokenAsync(request.Username, request.Password);
        }
    }
}
