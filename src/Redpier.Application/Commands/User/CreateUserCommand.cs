using MediatR;
using Redpier.Application.Common.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.User
{
    public class CreateUserCommand : IRequest<bool>
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.CreateAsync(
                request.Username,
                request.Password);
        }
    }
}
