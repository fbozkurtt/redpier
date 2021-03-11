using Redpier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Redpier.Application.Common.Interfaces.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<bool> CreateAsync(string userName, string password);

        Task<User> GetAsync(string userName);

        Task<bool> UpdateAsync(User user, string userName, string password);

        Task<bool> DeleteAsync(string userName);

        Task<string> GetUsernameAsync(Guid userId);

        Task<Guid> GetIdAsync(string userName);

        Task<bool> AddToRoleAsync(string userName, string roleName);
    }
}
