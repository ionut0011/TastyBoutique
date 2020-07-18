using System;
using System.Collections.Generic;

namespace TastyBoutique.Database.Models
{
    public partial class RecipesFilters
    {
        public int RecipeId { get; set; }
        public int FilterId { get; set; }

        public virtual Filters Filter { get; set; }
        public virtual Recipes Recipe { get; set; }
    }
}
