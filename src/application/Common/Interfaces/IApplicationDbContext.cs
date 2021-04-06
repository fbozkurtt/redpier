using Microsoft.EntityFrameworkCore;
using Redpier.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Event> Events { get; set; }

        public DbSet<DockerEndpoint> DockerEndpoints { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
