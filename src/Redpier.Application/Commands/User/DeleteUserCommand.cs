using MediatR;
using Redpier.Application.Common.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return request.Id.HasValue
                ? await _userRepository.DeleteAsync(request.Id.Value)
                : await _userRepository.DeleteByUsernameAsync(request.Username);
        }
    }
}
