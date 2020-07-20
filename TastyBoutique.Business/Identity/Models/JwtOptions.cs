using System;
using System.Collections.Generic;
using System.Text;

namespace TastyBoutique.Business.Identity.Models
{
    public sealed class JwtOptions
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string Key { get; set; }

        public string TokenExpirationInHours { get; set; }
    }
}
