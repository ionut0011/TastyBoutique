using System;
using System.Collections.Generic;

namespace TastyBoutique.Persistance.Models
{
    public partial class RecipeType
    {
        public Guid RecipeId { get; set; }
        public string Type { get; set; }

        public virtual Recipes Recipe { get; set; }
    }
}
