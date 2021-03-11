using Microsoft.EntityFrameworkCore;
using Redpier.Application.Common.Interfaces.Repositories;
using Redpier.Domain.Entities;
using Redpier.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.Infrastructure.Persistence.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AddToRoleAsync(string userName, string roleName)
        {
            var user = await GetAsync(userName);

            return await AddToRoleAsync(user, roleName);
        }

        public async Task<bool> AddToRoleAsync(User user, string roleName)
        {
            var role = await _dbContext.Roles.FirstAsync(w => w.Name.Equals(roleName));

            user.Roles.Add(role);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> CreateAsync(string userName, string password)
        {
            var user = new User
            {
                Username = userName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };

            await _dbContext.Users.AddAsync(user);

            return await AddToRoleAsync(user, "User");

        }

        public async Task<bool> DeleteAsync(string userName)
        {
            var user = await GetAsync(userName);

            _dbContext.Users.Remove(user);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<User> GetAsync(string userName)
        {
            return await _dbContext.Users
                .SingleOrDefaultAsync(w => w.Username.Equals(userName));
        }

        public async Task<Guid> GetIdAsync(string userName)
        {
            return await _dbContext.Users
                .Where(w => w.Username
                .Equals(userName))
                .Select(w => w.Id.Value)
                .FirstAsync();
        }

        public async Task<string> GetUsernameAsync(Guid userId)
        {
            return await _dbContext.Users
                .Where(w => w.Id.Equals(userId))
                .Select(w => w.Username)
                .FirstAsync();
        }

        public async Task<bool> IsInRoleAsync(string userName, string roleName)
        {
            var user = await GetAsync(userName);

            return user.Roles.Any(w => w.Name.Equals(roleName));
        }

        public async Task<bool> UpdateAsync(User user, string userName, string password)
        {
            throw new NotImplementedException();
            //user.Username = userName;

            //user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);

            ////_dbContext.Users.Update(user);

            //return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
