using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redpier.Domain.Entities;
using System;

namespace Redpier.Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasIndex(w => new { w.Name })
                   .IsUnique();

            builder.HasData(new Role()
            {
                Name = "Admin",
                Id = Guid.NewGuid(),
                Created = DateTime.Now
            });

            builder.HasData(new Role()
            {
                Name = "User",
                Id = Guid.NewGuid(),
                Created = DateTime.Now
            });
        }
    }
}
