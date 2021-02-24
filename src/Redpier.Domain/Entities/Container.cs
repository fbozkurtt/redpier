using Redpier.Domain.Common;

namespace Redpier.Domain.Entities
{
    public class Container : BaseEntity
    {
        public string Id { get; set; }

        public string ShortId { get; set; }

        public string[] Names { get; set; }

        //public string Command { get; set; }

        public Image Image { get; set; }
    }
}
