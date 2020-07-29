using System;
using System.Collections.Generic;

namespace TastyBoutique.Persistance.Models
{
    public partial class Notifications : Entity
    {
        public Guid IdRecipe { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Recipes IdRecipeNavigation { get; set; }
    }
}
