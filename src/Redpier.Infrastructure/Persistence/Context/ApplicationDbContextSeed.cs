using Microsoft.AspNetCore.Identity;
using Redpier.Application.Common.Interfaces;
using Redpier.Application.Common.Interfaces.Repositories;
using Redpier.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.Infrastructure.Persistence.Context
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDatabase(IApplicationDbContext dbContext)
        {
            var administratorRole = new Role() { Name = "Admin"};

            if (dbContext.Roles.All(r => r.Name != administratorRole.Name))
            {
                dbContext.Roles.Add(administratorRole);
            }

            var administrator = new User { Username = "admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("!Admin1")};

            if (dbContext.Users.All(u => u.Username != administrator.Username))
            {
                dbContext.Users.Add(administrator);
            }

            var UserRole = new UserRole
            {
                UserId = dbContext.Users.First(u => u.Username == administrator.Username).Id,
                RoleId = dbContext.Roles.First(w => w.Name == administratorRole.Name).Id
            };

            if (!dbContext.UserRoles.Contains(UserRole))
            {
                dbContext.UserRoles.Add(UserRole);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
