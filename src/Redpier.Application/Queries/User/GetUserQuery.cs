using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Redpier.Application.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.User
{
    public class GetUserQuery : IRequest<IdentityUser<Guid>>
    {
        public Guid? Id { get; set; }

        public string Username { get; set; }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, IdentityUser<Guid>>
    {
        private readonly IIdentityService _IdentityService;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IIdentityService IdentityService, IMapper mapper)
        {
            _IdentityService = IdentityService;
            _mapper = mapper;
        }

        public async Task<IdentityUser<Guid>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = request.Id.HasValue
                ? await _IdentityService.GetUserAsync(request.Id.Value)
                : await _IdentityService.GetUserAsync(request.Username);

            return user;
        }
    }
}
