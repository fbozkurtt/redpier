using MediatR;
using Redpier.Application.Common.Interfaces;
using Redpier.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Identity
{
    public class GetTokenCommand : IRequest<GetTokenResponse>
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class GetTokenCommandHandler : IRequestHandler<GetTokenCommand, GetTokenResponse>
    {
        private readonly IIdentityService _identityService;

        public GetTokenCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<GetTokenResponse> Handle(GetTokenCommand request, CancellationToken cancellationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = await _identityService.GetTokenAsync(
                request.Username,
                request.Password);

            return new GetTokenResponse()
            {
                Token = tokenHandler.WriteToken(token),
                Username = request.Username,
                Expires = token.ValidTo
            };
        }
    }
}
