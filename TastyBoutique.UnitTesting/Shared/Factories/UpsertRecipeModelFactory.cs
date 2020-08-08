using System;
using System.Collections.Generic;
using System.Text;
using TastyBoutique.Business.Models.Recipe;
using TastyBoutique.UnitTesting.Shared.Extensions;

namespace TastyBoutique.UnitTesting.Shared.Factories
{
    public static class UpsertRecipeModelFactory
    {

        public static UpsertRecipeModel Default()
        {
            return new UpsertRecipeModel()
            {
                Access = true, Description = "buntare", Filter = "mancare", Name = "test", Type = "asd",IdUser = new Guid(),
                IngredientsList = new List<string> {"apa", "mere"}
            };

        }

        public static UpsertRecipeModel WithNameNull()
        {
            return UpsertRecipeModelExtensions.WithName(Default(), null);
        }

        public static UpsertRecipeModel WithNameEmpty()
        {
            return UpsertRecipeModelExtensions.WithName(Default(), "");
        }

        public static UpsertRecipeModel WithNameGreaterThan50Characters()
        {
            return UpsertRecipeModelExtensions.WithName(Default(),
                "mWomMR6eTISDjMU6vzUX21pz4zyKjXerLKCSkfkA8RYt0lXI6yTs");

        }

        public static UpsertRecipeModel WithDescriptionGreaterThan300Characters()
        {
            return UpsertRecipeModelExtensions.WithDescription(Default(),
                "yIRM59R9ssbXOjx0a5Dvl91bujYMUOrn8X7bNiOaW5eVKtkLwB1pGoyCScBJaQ1OOSImGMUBs2L5yUIpzepPh5Vum1ShxooQt9Z9HR6xQLEbpfFymU5PYRmAaw8h8u1JSJ28G9dbjz8hUYwnwHVaDKdwVvSq9COYBG9u3RirIxGdQlDevL3GRPdzeobBMxaCY83vn0rxEXjPuklDN2metvKaF0ZqO52CuLfcndCV07lIQTopHSV6QD3ecmGJANdMMM83rAgI4ohRSjkYvdRJfXRb1Zy3ODEk1PHrg6dqqT3IM");
        }

        public static UpsertRecipeModel WithTypeGreaterThan50Characters()
        {
            return UpsertRecipeModelExtensions.WithType(Default(),
                "mWomMR6eTISDjMU6vzUX21pz4zyKjXerLKCSkfkA8RYt0lXI6yTs");
        }

        public static UpsertRecipeModel WithTypeEmpty()
        {
            return UpsertRecipeModelExtensions.WithType(Default(),
                "");
        }

        public static UpsertRecipeModel WithFilterEmpty()
        {
            return UpsertRecipeModelExtensions.WithFilter(Default(), "");
        }

        public static UpsertRecipeModel WithIngredientsListEmpty()
        {
            return UpsertRecipeModelExtensions.WithIngredientsList(Default(), new List<string> {});
        }

        public static UpsertRecipeModel WithIngredientsListNull()
        {
            return UpsertRecipeModelExtensions.WithIngredientsList(Default(), null);
        }
    }
}
