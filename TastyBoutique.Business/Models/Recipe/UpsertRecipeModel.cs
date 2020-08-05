using System;
using System.Collections.Generic;

namespace TastyBoutique.Business.Models.Recipe
{
    public sealed class UpsertRecipeModel
    {
        public string Name { get; set; }
        public Boolean Access { get; set; }
        public string Description { get; set; }

        public byte[] Image { get; set; }
        public int Type { get; set;  }

        public Guid IdUser { get; set; }

        public IList<string> IngredientsList { get; set; }

        public IList<string> FiltersList { get; set; }
    }
}
