using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;

namespace Redpier.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<IdentityUser<Guid>> GetUserAsync(Guid userId);

        Task<IdentityUser<Guid>> GetUserAsync(string username);

        Task<string> GetUserNameAsync(Guid userId);

        Task<bool> IsInRoleAsync(Guid userId, string role);

        Task<bool> AuthorizeAsync(Guid userId, string policyName);

        Task<Guid> CreateUserAsync(string username, string password);

        Task<bool> DeleteUserAsync(Guid userId);

        Task<bool> DeleteUserAsync(string username);

        Task<SecurityToken> GetTokenAsync(string username, string password);
    }
}
