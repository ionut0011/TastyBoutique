using System;
using System.Collections.Generic;

namespace TastyBoutique.Database.Models
{
    public partial class RecipesIngredients
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }

        public virtual Ingredients Ingredient { get; set; }
        public virtual Recipes Recipe { get; set; }
    }
}
