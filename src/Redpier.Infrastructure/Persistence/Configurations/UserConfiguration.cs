using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redpier.Domain.Entities;

namespace Redpier.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(w => new { w.Username })
                .IsUnique();

            builder.Property(w => w.LockoutEnabled)
                .HasDefaultValue(true);

            builder.Property(w => w.AccessFailedCount)
                .HasDefaultValue(0);
        }
    }
}
