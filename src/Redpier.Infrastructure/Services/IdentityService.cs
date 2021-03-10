using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Redpier.Application.Common.Interfaces;
using Redpier.Application.Common.Interfaces.Repositories;
using Redpier.Application.Common.Models;
using Redpier.Domain.Entities;
using Redpier.Infrastructure.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;

        public IdentityService(
            IUserRepository userRepository,
            IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService)
        {
            _userRepository = userRepository;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
        }

        public async Task<bool> AuthorizeAsync(string userName, string policyName)
        {
            var user = await _userRepository.GetByUsernameAsync(userName);//SingleOrDefault(u => u.Id == userId);

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }


        public async Task<string> GetUserNameAsync(Guid userId)
        {
            return await _userRepository.GetUserNameAsync(userId);
        }

        public async Task<bool> IsInRoleAsync(string userName, string roleName)
        {

            return await _userRepository.IsInRoleAsync(userName, roleName);
        }
    }
}
