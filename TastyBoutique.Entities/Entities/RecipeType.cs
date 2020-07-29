using System;
using System.Collections.Generic;

namespace TastyBoutique.Persistance.Models
{
    public partial class RecipeType
    {
        public RecipeType()
        {
        }

        public RecipeType(Recipes recipe, string type)
        {
            Type = type;
            Recipe = recipe;
        }

        public Guid RecipeId { get; set; }
        public string Type { get; set; }

        public Recipes Recipe { get; set; }
    }
}
