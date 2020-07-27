using System;
using System.Collections.Generic;
using System.Text;

namespace TastyBoutique.Business.Identity.Models
{
   public sealed  class AuthenticationResponse
    {
        
        public AuthenticationResponse(string Username, string Token, string Email)
        {
            this.Username = Username;
            this.Token = Token;
            this.Email = Email;
        }

        public string Username { get; private set; }

        public string Email { get; private set; }

        public string Token { get; private set; }


    }
}
