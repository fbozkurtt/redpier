using MediatR;
using Redpier.Application.Common.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Identity
{
    public class UpdateCredentialsCommand : IRequest<bool>
    {
        [MaxLength(64)]
        [MinLength(5)]
        public string Username { get; set; }

        [MaxLength(64)]
        [MinLength(5)]
        public string Password { get; set; }
    }

    public class UpdateCredentialsCommandHandler : IRequestHandler<UpdateCredentialsCommand, bool>
    {
        private readonly ICurrentUserService _currentUserService;

        public UpdateCredentialsCommandHandler(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(UpdateCredentialsCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            //var user = await _userRepository.GetAsync(_currentUserService.UserId);

            //if (user == null)
            //    throw new NotFoundException(nameof(Domain.Entities.User), nameof(user.Id), _currentUserService.UserId);

            //return await _userRepository.UpdateAsync(user, request.Username, request.Password);
        }
    }
}
