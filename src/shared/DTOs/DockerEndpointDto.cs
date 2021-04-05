using Redpier.Domain.Entities;
using Redpier.Shared.Mappings;
using System;
using System.ComponentModel;

namespace Redpier.Shared.DTOs
{
    [DisplayName("endpoint")]
    public class DockerEndpointDto : IMapFrom<DockerEndpoint>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }

        public string Type { get; set; }
    }
}
