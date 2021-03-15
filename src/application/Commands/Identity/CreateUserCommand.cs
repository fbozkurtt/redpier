using MediatR;
using Redpier.Application.Common.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Identity
{
    public class CreateUserCommand : IRequest<Guid>
    {
        [Required]
        [MaxLength(64)]
        [MinLength(5)]
        public string Username { get; set; }

        [Required]
        [MinLength(5)]
        public string Password { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IIdentityService _IdentityService;

        public CreateUserCommandHandler(IIdentityService IdentityService)
        {
            _IdentityService = IdentityService;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await _IdentityService.CreateUserAsync(
                request.Username,
                request.Password);
        }
    }
}
