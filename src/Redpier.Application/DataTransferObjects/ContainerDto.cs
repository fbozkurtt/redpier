using Redpier.Application.Common.Mappings;
using Redpier.Domain.Entities;

namespace Redpier.Application.DataTransferObjects
{
    public class ContainerDto : IMapFrom<Container>
    {
        public string Id { get; set; }

        public string ShortId { get; set; }

        public string[] Names { get; set; }

        //public string Command { get; set; }

        public Image Image { get; set; }

    }
}
