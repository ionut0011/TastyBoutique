using TastyBoutique.Business.Models.Ingredients;
using TastyBoutique.UnitTesting.Shared.Extensions;

namespace TastyBoutique.UnitTesting.Shared.Factories
{
   public static class CreateIngredientModelFactory
    {
        public static CreateIngredientModel Default()
        {
            return new CreateIngredientModel() { Name = "NumeTest1" };
        }

        public static CreateIngredientModel WithNameNull()
        {
            return CreateIngredientModelExtensions.WithName(Default(), null);

        }

        public static CreateIngredientModel WithNameEmpty()
        {
            return CreateIngredientModelExtensions.WithName(Default(), "");

        }

        public static CreateIngredientModel WithNameHavingMoreThan50Characters()
        {
            return CreateIngredientModelExtensions.WithName(Default(), "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

        }
    }
}
