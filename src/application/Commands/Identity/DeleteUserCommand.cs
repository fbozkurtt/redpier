using MediatR;
using Microsoft.AspNetCore.Authorization;
using Redpier.Application.Common.Interfaces;
using Redpier.Shared.Constants;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Identity
{
    [Authorize(Roles = DefaultRoleNames.User)]
    public class DeleteUserCommand : IRequest<bool>
    {
        public Guid? Id { get; set; }

        public string Username { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;

        public DeleteUserCommandHandler(
            IIdentityService identityService,
            ICurrentUserService currentUserService)
        {
            _identityService = identityService;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {

            //if (user == null)
            //    throw new NotFoundException(nameof(Domain.Entities.User), nameof(user.Id), request.Id);


            //if (!_currentUserService.UserId.Equals(user.Id) || _identityService.IsInRoleAsync(_currentUserService.Username, "Admin").Result)
            //    throw new UnauthorizedAccessException();

            return request.Id.HasValue
                ? await _identityService.DeleteUserAsync(request.Id.Value)
                : await _identityService.DeleteUserAsync(request.Username);
        }
    }
}
