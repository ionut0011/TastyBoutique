using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using TastyBoutique.Business.Identity.Models;

namespace TastyBoutique.UnitTests.Shared.Extensions
{
    public static class UserRegisterModelExtensions
    {

        public static UserRegisterModel WithEmail(this UserRegisterModel model,string email)
        {
            model.Email = email;
            return model;
        }

        public static UserRegisterModel WithUsername(this UserRegisterModel model, string username)
        {
            model.Username = username;
            return model;
        }

        public static UserRegisterModel WithPassword(this UserRegisterModel model, string password)
        {
            model.Password = password;
            return model;
        }


    }
}
