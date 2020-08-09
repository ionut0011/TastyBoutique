using System;
using System.Collections.Generic;

namespace TastyBoutique.Persistance.Models
{
    public class SavedRecipes : Entity
    {
        public Guid IdRecipe { get; set; }
        public Guid IdUser { get; set; }

        public bool NeedUpdate { get; set; }
        public virtual Recipes IdRecipeNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
