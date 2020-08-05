using System;

namespace TastyBoutique.Business.Models.Recipe
{
    public class SavedRecipeModel
    {
        public Guid IdRecipe { get; set; }
        public Guid IdUser { get; set; }
    }
}
