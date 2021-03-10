using Redpier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redpier.Application.Common.Interfaces.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<bool> CreateAsync(string userName, string password);

        Task<User> GetByUsernameAsync(string userName);

        Task<bool> UpdateAsync(string userName, string password);

        Task<bool> DeleteByUsernameAsync(string userName);

        Task<string> GetUserNameAsync(Guid userId);

        Task<bool> IsInRoleAsync(string userName, string roleName);

        Task<IList<Role>> GetRolesAsync(string userName);

        Task<bool> AddToRoleAsync(string userName, string roleName);
    }
}
