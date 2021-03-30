using System;

namespace Redpier.Application.DTOs
{
    public class DockerEndpointDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }

        public string Type { get; set; }
    }
}
