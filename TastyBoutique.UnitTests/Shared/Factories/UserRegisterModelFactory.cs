
using TastyBoutique.Business.Identity.Models;
using TastyBoutique.UnitTests.Shared.Extensions;

namespace TastyBoutique.UnitTests.Shared.Factories
{
   public static class UserRegisterModelFactory
    {

        public static UserRegisterModel Default()
        {
            return new UserRegisterModel() { Age = 22, Email = "test@yahoo.com", Name = "Test123", Password = "12345", Username = "tester" };
        }

        public static UserRegisterModel WithEmailNull()
        {
            //return Default().WithEmail(null);

            return UserRegisterModelExtensions.WithEmail(Default(), null);
        }

        public static UserRegisterModel WithUsernameNull()
        {
            return UserRegisterModelExtensions.WithUsername(Default(), null);
        }

        public static UserRegisterModel WithPasswordNull()
        {
            return UserRegisterModelExtensions.WithPassword(Default(), null);
        }
    }
}
