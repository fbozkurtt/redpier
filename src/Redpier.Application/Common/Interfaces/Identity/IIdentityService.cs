using Redpier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redpier.Application.Common.Interfaces.Identity
{
    public interface IIdentityService
    {
        Task<string> GetTokenAsync(string username, string password);

        Task<bool> ValidateAsync(User user, string password);

        Task<bool> IsInRoleAsync(string username, string roleName);

        Task<bool> IsInRoleAsync(User user, string roleName);

        Task<bool> AddToRoleAsync(string username, string roleName);

        Task<IList<Role>> GetRoles(string username);
    }
}
