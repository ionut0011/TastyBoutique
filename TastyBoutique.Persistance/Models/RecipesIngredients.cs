using System;
using System.Collections.Generic;

namespace TastyBoutique.Persistance.Models
{
    public partial class RecipesIngredients
    {
        public RecipesIngredients()
        {

        }

        public RecipesIngredients(Recipes recipe, Ingredients ingredient)
        {
            Ingredient = ingredient;
            Recipe = recipe;
        }

        public Guid RecipeId { get; set; }
        public Guid IngredientId { get; set; }

        public Ingredients Ingredient { get; set; }
        public Recipes Recipe { get; set; }
    }
}
