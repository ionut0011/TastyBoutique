
using TastyBoutique.Business.Models.Filter;
using TastyBoutique.UnitTesting.Shared.Extensions;

namespace TastyBoutique.UnitTesting.Shared.Factories
{
    public static class CreateFilterModelFactory
    {

        public static CreateFilterModel Default()
        {
            return new CreateFilterModel() {Name = "Fara restrictii"};
        }

        public static CreateFilterModel WithNameNull()
        {
            return CreateFilterModelExtensions.WithName(Default(), null);

        }

        public static CreateFilterModel WithNameEmpty()
        {
            return CreateFilterModelExtensions.WithName(Default(), "");

        }

        public static CreateFilterModel WithNameHavingMoreThan50Characters()
        {
            return CreateFilterModelExtensions.WithName(Default(), "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

        }
    }
}
