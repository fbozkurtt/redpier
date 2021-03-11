using MediatR;
using Redpier.Application.Common.Interfaces;
using Redpier.Application.Common.Interfaces.Identity;
using Redpier.Application.Common.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.User
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public Guid? Id { get; set; }

        public string Username { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;

        public DeleteUserCommandHandler(
            IUserRepository userRepository,
            IIdentityService identityService,
            ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _identityService = identityService;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.Id.HasValue
                ? await _userRepository.GetAsync(request.Id.Value)
                : await _userRepository.GetAsync(request.Username);

            if (!_currentUserService.UserId.Equals(user.Id) || _identityService.IsInRoleAsync(_currentUserService.Username, "Admin").Result)
                throw new UnauthorizedAccessException();

            return await _userRepository.DeleteAsync(user);
        }
    }
}
