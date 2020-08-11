using System;
using TastyBoutique.Business.Models.Filter;
using TastyBoutique.Business.Models.Ingredients;
using TastyBoutique.Business.Models.Recipe;


namespace TastyBoutique.UnitTesting.Shared.Extensions
{
   public static  class TotalRecipeModelExtensions
    {


        public static TotalRecipeModel WithName(this TotalRecipeModel model, string name)
        {
            model.Name = name;
            return model;
        }

        public static TotalRecipeModel WithDescription(this TotalRecipeModel model, string description)
        {
            model.Description = description;
            return model;
        }
        public static TotalRecipeModel WithType(this TotalRecipeModel model, string type)
        {
            model.Type = type;
            return model;
        }
        public static TotalRecipeModel WithRecipesIngredients(this TotalRecipeModel model, IngredientModel[] recipeIngredients)
        {
            model.Ingredients = recipeIngredients;
            return model;
        }
        public static TotalRecipeModel WithRecipesFilters(this TotalRecipeModel model, FilterModel[] recipeFilters)
        {
            model.Filters = recipeFilters;
            return model;
        }

        public static TotalRecipeModel WithAverageReview(this TotalRecipeModel model, double averageReview)
        {
            model.AverageReview = Int16.Parse(""+averageReview);
            return model;
        }
    }
}
