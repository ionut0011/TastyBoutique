using System;
using System.Collections.Generic;

namespace TastyBoutique.Database.Models
{
    public partial class Recipes
    {
        public Recipes()
        {
            NotificationsNavigation = new HashSet<Notifications>();
            RecipeComment = new HashSet<RecipeComment>();
            SavedRecipes = new HashSet<SavedRecipes>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Access { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public string Link { get; set; }
        public string Notifications { get; set; }

        public virtual ICollection<Notifications> NotificationsNavigation { get; set; }
        public virtual ICollection<RecipeComment> RecipeComment { get; set; }
        public virtual ICollection<SavedRecipes> SavedRecipes { get; set; }
    }
}
