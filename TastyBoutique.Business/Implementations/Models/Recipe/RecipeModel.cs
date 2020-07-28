using System;
using System.Collections.Generic;
using System.Text;

namespace TastyBoutique.Business.Recipes.Models.Recipe
{
    public sealed class RecipeModel
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Access { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public string Link { get; set; }
        public string Notifications { get; set; }

        public string Type { get; set; }
    }
}
