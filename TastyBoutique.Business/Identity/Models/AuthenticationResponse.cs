namespace TastyBoutique.Business.Identity.Models
{
   public sealed class AuthenticationResponse
    {
        public AuthenticationResponse(string username, string token, string email)
        {
            this.Username = username;
            this.Token = token;
            this.Email = email;
        }

        public string Username { get; private set; }

        public string Email { get; private set; }

        public string Token { get; private set; }


    }
}
