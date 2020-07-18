using System;
using System.Collections.Generic;

namespace TastyBoutique.Database.Models
{
    public partial class RecipeType
    {
        public int RecipeId { get; set; }
        public string Type { get; set; }

        public virtual Recipes Recipe { get; set; }
    }
}
