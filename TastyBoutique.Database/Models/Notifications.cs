using System;
using System.Collections.Generic;

namespace TastyBoutique.Database.Models
{
    public partial class Notifications
    {
        public int Id { get; set; }
        public int IdRecipe { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Recipes IdRecipeNavigation { get; set; }
        
    }
}
