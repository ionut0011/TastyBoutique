using System;

namespace TastyBoutique.Persistance.Models
{
    public partial class RecipesFilters 
    {
        public RecipesFilters()
        {
        }

        public RecipesFilters(Recipes recipe, Filters filter)
        {
            Filter = filter;
            Recipe = recipe;
        }

        public Guid RecipeId { get; set; }
        public Guid FilterId { get; set; }

        public virtual Filters Filter { get; set; }
        public virtual Recipes Recipe { get; set; }
    }
}
