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
        public Boolean Access { get; set; }
        public string Description { get; set; }

        public byte[] Image { get; set; }
        public string Type { get; set;  }

        public Guid IdUser { get; private set; }

        public IList<string> IngredientsList { get; set; }

        public string Filter { get; set; }
    }
}
