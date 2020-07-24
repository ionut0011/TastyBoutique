using System;
using System.Collections.Generic;

namespace TastyBoutique.Persistance.Models
{
    public partial class RecipesIngredients
    {
       

        public Guid RecipeId { get; set; }
        public Guid IngredientId { get; set; }

        public virtual Ingredients Ingredient { get; set; }
        public virtual Recipes Recipe { get; set; }
    }
}
