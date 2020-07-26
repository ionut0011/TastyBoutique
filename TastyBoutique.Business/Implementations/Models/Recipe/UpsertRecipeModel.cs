using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using TastyBoutique.Business.Recipes.Models.Ingredients;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Business.Recipes.Models.Recipe
{
    public sealed class UpsertRecipeModel
    {
        public string Name { get; set; }
        public string Access { get; set; }
        public string Description { get; set; }

        public byte[] Image { get; set; }

        public string Link { get; set; }
        public string Notifications { get; set; }

        public IList<Persistance.Models.Ingredients> IngredientsList { get; set; }

        public IList<Filters> FiltersList { get; set; }
    }
}
