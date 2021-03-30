using MediatR;
using Redpier.Application.Common.Interfaces;
using Redpier.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Identity
{
    public class GetTokenCommand : IRequest<LoginResponse>
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class GetTokenCommandHandler : IRequestHandler<GetTokenCommand, LoginResponse>
    {
        private readonly IIdentityService _identityService;

        public GetTokenCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<LoginResponse> Handle(GetTokenCommand request, CancellationToken cancellationToken)
        {
            var token = await _identityService.GetTokenAsync(
                request.Username,
                request.Password);
            return new LoginResponse() { Token = token, Username = request.Username };
        }
    }
}
