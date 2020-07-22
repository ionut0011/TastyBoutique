using System;
using System.Collections.Generic;
using System.Text;

namespace TastyBoutique.Business.Recipes.Models.Ingredients
{
    public sealed class IngredientModel
    {

        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
