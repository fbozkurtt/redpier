using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redpier.Domain.Entities;
using System;

namespace Redpier.Infrastructure.Persistence.Configurations
{
    public class DockerEndpointConfiguration : IEntityTypeConfiguration<DockerEndpoint>
    {
        public void Configure(EntityTypeBuilder<DockerEndpoint> builder)
        {
            builder.HasIndex(w => new { w.Uri })
                   .IsUnique();

            builder.HasData(new DockerEndpoint()
            {
                Name="Docker Desktop for Windows",
                Uri = "npipe://./pipe/docker_engine",
                Type ="socket",
                Id = Guid.NewGuid(),
                Created = DateTime.Now
            });

            builder.HasData(new DockerEndpoint()
            {
                Name="Local Docker Engine on Linux",
                Uri = "/var/run/docker.sock",
                Type = "socket",
                Id = Guid.NewGuid(),
                Created = DateTime.Now
            });
        }
    }
}
