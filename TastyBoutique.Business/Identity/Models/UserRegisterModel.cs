using System;
using System.Collections.Generic;
using System.Text;

namespace TastyBoutique.Business.Identity.Models
{
    public sealed class UserRegisterModel
    {
        public CreateStudentModel studentModel { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

    }
}
