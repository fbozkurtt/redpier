using Redpier.Application.Common.Models;
using Redpier.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Redpier.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(Guid userId);

        Task<bool> IsInRoleAsync(string userName, string roleName);

        Task<bool> AuthorizeAsync(string userName, string policyName);
    }
}
