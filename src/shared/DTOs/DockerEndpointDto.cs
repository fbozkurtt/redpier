using Redpier.Domain.Entities;
using Redpier.Shared.Mappings;
using System;
using System.ComponentModel;

namespace Redpier.Shared.DTOs
{
    public class DockerEndpointDto : DtoBase, IMapFrom<DockerEndpoint>
    {
        public string Name { get; set; }

        public string Uri { get; set; }

        public string Type { get; set; }
    }
}