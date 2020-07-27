using System;
using System.Collections.Generic;
using System.Text;

namespace TastyBoutique.Business.Identity.Models
{
    public sealed class AuthenticationRequest
    {

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
