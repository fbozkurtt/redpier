using System.ComponentModel.DataAnnotations;

namespace Redpier.Web.UI.Models
{
    public class LoginModel
    {
        [Required]
        [StringLength(maximumLength: 64, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [StringLength(maximumLength: 256, MinimumLength = 5)]
        public string Password { get; set; }
    }
}
