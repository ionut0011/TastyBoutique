using System;
using System.Collections.Generic;
using System.Text;
using TastyBoutique.Business.Recipes.Models.Recipe;

namespace TastyBoutique.Business.Collections.Models
{
    public sealed class CollectionModel
    {
        public ICollection<SavedRecipeModel> Recipes;
    }
}
