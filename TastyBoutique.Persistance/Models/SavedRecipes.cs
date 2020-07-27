using System;
using System.Collections.Generic;

namespace TastyBoutique.Persistance.Models
{
    public partial class SavedRecipes : Entity
    {
        public Guid IdRecipe { get; set; }
        public Guid IdUser { get; set; }

        public Boolean Version { get; set; }
        public virtual Recipes IdRecipeNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
