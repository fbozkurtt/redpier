using AutoMapper;
using MediatR;
using Redpier.Application.Common.Interfaces.Repositories;
using Redpier.Application.DataTransferObjects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.User
{
    public class GetUserQuery : IRequest<UserDto>
    {
        public Guid? Id { get; set; }

        public string Username { get; set; }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = request.Id.HasValue
                ? await _userRepository.GetAsync(request.Id.Value)
                : await _userRepository.GetAsync(request.Username);

            return _mapper.Map<UserDto>(user);
        }
    }
}
