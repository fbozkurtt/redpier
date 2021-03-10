using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redpier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Uri = "npipe://./pipe/docker_engine",
                Description = "Default Docker Desktop endpoint for Windows."
            });

            builder.HasData(new DockerEndpoint()
            {
                Uri = "/var/run/docker.sock",
                Description = "Default Docker endpoint for Linux."
            });
        }
    }
}
