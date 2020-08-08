
using TastyBoutique.Business.Identity.Models;
using TastyBoutique.UnitTesting.Shared.Extensions;

namespace TastyBoutique.UnitTesting.Shared.Factories
{
    public static class UserRegisterModelFactory
    {

        public static UserRegisterModel Default()
        {
            return new UserRegisterModel() 
                {Age = 16, Email = "test@gmail.com", Name = "George", Password = "Serioux22", Username = "tester"};
        }

        public static UserRegisterModel WithEmailNull()
        {
            return UserRegisterModelExtensions.WithEmail(Default(),null);
        }

        public static UserRegisterModel WithEmailEmpty()
        {
            return UserRegisterModelExtensions.WithEmail(Default(), "");
        }

        public static UserRegisterModel WithEmailHavingMoreThan50Characters()
        {
            return UserRegisterModelExtensions.WithEmail(Default(),
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa@yahoo.com");
        }

        public static UserRegisterModel WithInvalidEmailExtension()
        {
            return UserRegisterModelExtensions.WithEmail(Default(), "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        }

        public static UserRegisterModel WithPasswordNull()
        {
            return UserRegisterModelExtensions.WithPassword(Default(), null);
        }

        public static UserRegisterModel WithPasswordEmpty()
        {
            return UserRegisterModelExtensions.WithPassword(Default(), "");
        }

        public static UserRegisterModel WithPasswordNotMatchingRegex()
        {
            return UserRegisterModelExtensions.WithPassword(Default(), "asd");
        }

        public static UserRegisterModel WithUsernameNull()
        {
            return UserRegisterModelExtensions.WithUsername(Default(), null);
        }

        public static UserRegisterModel WithUsernameEmpty()
        {
            return UserRegisterModelExtensions.WithUsername(Default(), "");
        }

        public static UserRegisterModel WithUsernameHavingMoreThan50Characters()
        {
            return UserRegisterModelExtensions.WithUsername(Default(), "supertestersupertestersupertestersupertestersupertestersupertestersupertester");
        }

        public static UserRegisterModel WithAgeBelow14()
        {
            return UserRegisterModelExtensions.WithAge(Default(), 13);
        }
    }
}
