using System.ComponentModel.DataAnnotations;

namespace Redpier.Web.UI.Models
{
    public class AddEndpointModel
    {
        [Required]
        [MaxLength(2048)]
        public string Uri { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
    }
}
