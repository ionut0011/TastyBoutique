using System;
using System.Collections.Generic;
using System.Text;

namespace TastyBoutique.Business.Identity.Models
{
    public sealed class UserModel
    {
        public Guid IdUser { get; set; }

        public Guid IdStudent { get; set; }
        public Guid IdUserType { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
    }
}
