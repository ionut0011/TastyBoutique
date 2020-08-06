using System;
using System.Collections.Generic;

namespace TastyBoutique.Persistance.Models
{
    public partial class Ingredients : Entity
    {
        public Ingredients()
        {

        }

        public Ingredients(string name)
        {
            Name = name;
            RecipesIngredients = new HashSet<RecipesIngredients>();
        }

        public string Name { get; set; }

        public virtual ICollection<RecipesIngredients> RecipesIngredients { get; set; }
    }
}
