using System.Collections.Generic;
using TastyBoutique.Business.Models.Recipe;

namespace TastyBoutique.UnitTesting.Shared.Extensions
{
    public static class UpsertRecipeModelExtensions
    {

        public static UpsertRecipeModel WithName(this UpsertRecipeModel model, string name)
        {
            model.Name = name;
            return model;
        }

        public static UpsertRecipeModel WithDescription(this UpsertRecipeModel model, string description)
        {
            model.Description = description;
            return model;
        }

        public static UpsertRecipeModel WithType(this UpsertRecipeModel model, string type)
        {
            model.Type = type;
            return model;
        }

        public static UpsertRecipeModel WithIngredientsList(this UpsertRecipeModel model, List<string> ingredientsList)
        {
            model.IngredientsList = ingredientsList;
            return model;
        }

        public static UpsertRecipeModel WithFilter(this UpsertRecipeModel model, string filter)
        {
            model.Filter = filter;
            return model;
        }
    }
}
