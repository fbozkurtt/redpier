using Microsoft.EntityFrameworkCore;
using Redpier.Application.Common.Interfaces;
using Redpier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.Infrastructure.Persistence.Context
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDatabase(IApplicationDbContext dbContext)
        {
            var administrator = new User { 
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("!Admin1"),
                Id = Guid.NewGuid()
            };

            administrator.Roles = new List<Role>
            {
                dbContext.Roles.Where(w => w.Name == "Admin").SingleOrDefault()
            };

            if (dbContext.Users.All(u => u.Username != administrator.Username))
            {
                dbContext.Users.Add(administrator);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
