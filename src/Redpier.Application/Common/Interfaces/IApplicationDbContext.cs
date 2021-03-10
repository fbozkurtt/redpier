using Microsoft.EntityFrameworkCore;
using Redpier.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<RoleClaim> RoleClaims { get; set; }

        public DbSet<DockerEndpoint> DockerEndpoints { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
