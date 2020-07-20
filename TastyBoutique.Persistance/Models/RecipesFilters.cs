using System;
using System.Collections.Generic;

namespace TastyBoutique.Persistance.Models
{
    public partial class RecipesFilters 
    {
        public Guid RecipeId { get; set; }
        public Guid FilterId { get; set; }

        public virtual Filters Filter { get; set; }
        public virtual Recipes Recipe { get; set; }
    }
}
