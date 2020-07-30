using System;
using System.Collections.Generic;
using System.Text;

namespace TastyBoutique.Business.Identity.Models
{
    public sealed class UserNewPasswordModel
    {
        public string Email { get; set; }

        public string NewPassword { get; set; }
        

    }
}