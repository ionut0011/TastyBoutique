using System;
using System.Collections.Generic;

namespace TastyBoutique.Database.Models
{
    public partial class SavedRecipes
    {
        public int Id { get; set; }
        public int IdRecipe { get; set; }
        public int IdUser { get; set; }

        public virtual Recipes IdRecipeNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
