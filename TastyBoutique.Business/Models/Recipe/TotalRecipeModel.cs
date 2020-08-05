using System;
using System.Collections.Generic;
using TastyBoutique.Business.Models.Filter;
using TastyBoutique.Business.Models.Ingredients;

namespace TastyBoutique.Business.Models.Recipe
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
