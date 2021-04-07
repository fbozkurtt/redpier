using Redpier.Application.Common.Mappings;
using Redpier.Domain.Entities;

namespace Redpier.Application.DTOs
{
    public class DockerEndpointDto : DtoBase, IMapFrom<DockerEndpoint>
    {
        public string Name { get; set; }

        public string Uri { get; set; }

        public string Type { get; set; }
    }
}