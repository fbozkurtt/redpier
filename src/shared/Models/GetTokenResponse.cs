using System;

namespace Redpier.Shared.Models
{
    public class GetTokenResponse
    {
        public string Username { get; set; }

        public string Token { get; set; }

        public DateTime Expires { get; set; }
    }
}
