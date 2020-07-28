using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using TastyBoutique.Business.Implementations.Models.Filter;
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

        public int Type { get; set;  }

        public IList<CreateIngredientModel> IngredientsList { get; set; }

        public IList<CreateFilterModel> FiltersList { get; set; }
    }
}
