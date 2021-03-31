using Redpier.Domain.Entities;
using Redpier.Shared.Mappings;
using System;

namespace Redpier.Shared.DTOs
{
    public class DockerEndpointDto : IMapFrom<DockerEndpoint>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }

        public string Type { get; set; }
    }
}
