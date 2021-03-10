using Redpier.Application.Common.Interfaces.Repositories;
using Redpier.Domain.Entities;
using Redpier.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redpier.Infrastructure.Persistence.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> AddToRoleAsync(string userName, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateAsync(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByUsernameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByUsernameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Role>> GetRolesAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(string userName, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
