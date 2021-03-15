using MediatR;
using Redpier.Application.Common.Interfaces;
using System.ComponentModel.DataAnnotations;
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
            return await _identityService.GetTokenAsync(
                request.Username,
                request.Password);
        }
    }
}
