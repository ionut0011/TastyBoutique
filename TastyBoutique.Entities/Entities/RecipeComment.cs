﻿using System;

namespace TastyBoutique.Persistance.Models
{
    public class RecipeComment : Entity
    {
        public Guid IdRecipe { get; set; }
        public Guid IdUser { get; set; }
        public string Comment { get; set; }
        public int Review { get; set; }

        public virtual Recipes IdRecipeNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
