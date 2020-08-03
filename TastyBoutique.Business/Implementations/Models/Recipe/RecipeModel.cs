using System;
using System.Collections.Generic;
using System.Text;
using TastyBoutique.Business.Implementations.Models.Filter;
using TastyBoutique.Business.Recipes.Models.Ingredients;

namespace TastyBoutique.Business.Recipes.Models.Recipe
{
    public sealed class RecipeModel
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public Boolean Access { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public string Type { get; set; }

        public Guid IdUser { get; set; }
        public IList<IngredientModel> IngredientsList { get; set; }

        public IList<FilterModel> FiltersList { get; set; }
    }
}
