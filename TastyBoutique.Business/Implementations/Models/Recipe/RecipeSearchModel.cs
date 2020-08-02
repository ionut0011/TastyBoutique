using System;
using System.Collections.Generic;
using System.Text;

namespace TastyBoutique.Business.Implementations.Models
{
    public sealed class RecipeSearchModel
    {
        public IList<string> IngredientsList { get; set; }
        public IList<string> FiltersList { get; set; }
    }
}
