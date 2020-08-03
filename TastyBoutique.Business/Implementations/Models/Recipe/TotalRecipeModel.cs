using System;
using System.Collections.Generic;
using System.Text;
using TastyBoutique.Business.Implementations.Models.Filter;
using TastyBoutique.Business.Recipes.Models.Ingredients;

namespace TastyBoutique.Business.Implementations.Models.Recipe
{
    public sealed class TotalRecipeModel
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public Boolean Access { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public string Type { get; set; }

        public IList<IngredientModel> Ingredients { get; set; }

        public IList<FilterModel> Filters { get; set; }
    }
}
