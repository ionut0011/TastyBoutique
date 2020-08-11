using System;
using System.Collections.Generic;
using System.Text;
using TastyBoutique.Business.Models.Filter;
using TastyBoutique.Business.Models.Ingredients;
using TastyBoutique.Business.Models.Recipe;
using TastyBoutique.Persistance.Models;
using TastyBoutique.UnitTesting.Shared.Extensions;

namespace TastyBoutique.UnitTesting.Shared.Factories
{
    public static class TotalRecipeModelFactory
    {
        
        public static TotalRecipeModel Default()
        {
            return new TotalRecipeModel()
            {
                Access = true, AverageReview = 3, Description = "categorie",
                Name = "Test", Type = "mancare",
                RecipesIngredients = new IngredientModel[2],RecipesFilters = new FilterModel[2]
            };
        }

        public static TotalRecipeModel WithAverageReviewLowerThanZero()
        {
            return TotalRecipeModelExtensions.WithAverageReview(Default(), -1);
        }

        public static TotalRecipeModel WithAverageReviewGreaterThan5()
        {
            return TotalRecipeModelExtensions.WithAverageReview(Default(), 6);
        }

        public static TotalRecipeModel WithNameNull()
        {
            return TotalRecipeModelExtensions.WithName(Default(), null);
        }

        public static TotalRecipeModel WithNameEmpty()
        {
            return TotalRecipeModelExtensions.WithName(Default(), "");
        }

        public static TotalRecipeModel WithNameGreaterThan50Characters()
        {
            return TotalRecipeModelExtensions.WithName(Default(),
                "mWomMR6eTISDjMU6vzUX21pz4zyKjXerLKCSkfkA8RYt0lXI6yTs");
            
        }

        public static TotalRecipeModel WithDescriptionGreaterThan300Characters()
        {
            return TotalRecipeModelExtensions.WithDescription(Default(),
                "yIRM59R9ssbXOjx0a5Dvl91bujYMUOrn8X7bNiOaW5eVKtkLwB1pGoyCScBJaQ1OOSImGMUBs2L5yUIpzepPh5Vum1ShxooQt9Z9HR6xQLEbpfFymU5PYRmAaw8h8u1JSJ28G9dbjz8hUYwnwHVaDKdwVvSq9COYBG9u3RirIxGdQlDevL3GRPdzeobBMxaCY83vn0rxEXjPuklDN2metvKaF0ZqO52CuLfcndCV07lIQTopHSV6QD3ecmGJANdMMM83rAgI4ohRSjkYvdRJfXRb1Zy3ODEk1PHrg6dqqT3IM");
        }

        public static TotalRecipeModel WithTypeGreaterThan50Characters()
        {
            return TotalRecipeModelExtensions.WithType(Default(),
                "mWomMR6eTISDjMU6vzUX21pz4zyKjXerLKCSkfkA8RYt0lXI6yTs");
        }

        public static TotalRecipeModel WithTypeEmpty()
        {
            return TotalRecipeModelExtensions.WithType(Default(),
                "");
        }

        public static TotalRecipeModel WithRecipeIngredientsNull()
        {
            return TotalRecipeModelExtensions.WithRecipesIngredients(Default(), null);
        }

        public static TotalRecipeModel WithRecipeIngredientsEmpty()
        {
            return TotalRecipeModelExtensions.WithRecipesIngredients(Default(),new IngredientModel[0]);
        }


        public static TotalRecipeModel WithRecipeFiltersEmpty()
        {
            return TotalRecipeModelExtensions.WithRecipesFilters(Default(), new FilterModel[0]);
        }





    }
}
