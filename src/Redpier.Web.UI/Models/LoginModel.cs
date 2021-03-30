using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
