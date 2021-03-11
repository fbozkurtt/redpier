using FluentValidation;
using MediatR;
using Redpier.Application.Common.Interfaces;
using Redpier.Application.Common.Interfaces.Identity;
using Redpier.Application.Common.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.User
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateUserCommandHandler(IUserRepository userRepository, ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(_currentUserService.UserId);

            return await _userRepository.UpdateAsync(user, request.Username, request.Password);
        }
    }
}
