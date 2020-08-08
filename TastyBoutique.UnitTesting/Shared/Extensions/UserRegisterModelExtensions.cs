using System;
using System.Collections.Generic;
using System.Text;
using TastyBoutique.Business.Identity.Models;

namespace TastyBoutique.UnitTesting.Shared.Extensions
{
    public static class UserRegisterModelExtensions
    {

        public static UserRegisterModel WithEmail(this UserRegisterModel model, string email)
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

        public static UserRegisterModel WithAge(this UserRegisterModel model, int age)
        {
            model.Age = age;
            return model;
        }
    }
}
