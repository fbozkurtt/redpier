using Microsoft.EntityFrameworkCore;
using Redpier.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        //DbSet<Container> Containers { get; set; }

        //DbSet<Image> Images { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
