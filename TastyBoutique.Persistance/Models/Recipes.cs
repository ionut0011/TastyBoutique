using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace TastyBoutique.Persistance.Models
{
    public sealed class Recipes : Entity
    {
        public Recipes() {

        }

        public Recipes(string name, string access, string description, byte[] image, string link, string notifications)
        {
            Name = name;
            Access = access;
            Description = description;
            Image = image;
            Link = link;
            Notifications = notifications;
            Version = 1;
            NotificationsNavigation = new HashSet<Notifications>();
            RecipeComment = new HashSet<RecipeComment>();
            SavedRecipes = new HashSet<SavedRecipes>();
            RecipesIngredients = new HashSet<RecipesIngredients>();
            RecipesFilters = new HashSet<RecipesFilters>();
        }


        public string Name { get; set; }
        public string Access { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public string Link { get; set; }
        public string Notifications { get; set; }

        public int Version { get; set; }
        public ICollection<Notifications> NotificationsNavigation { get; set; }
        public ICollection<RecipeComment> RecipeComment { get; set; }
        public ICollection<SavedRecipes> SavedRecipes { get; set; }

        public ICollection<RecipesIngredients> RecipesIngredients { get; set; }

        public ICollection<RecipesFilters> RecipesFilters { get; set; }

        public void Update(string name, string access, string description, byte[] image, string link,
            string notifications)
        {
            Name = name;
            Access = access;
            Description = description;
            Image = image;
            Link = link;
            Notifications = notifications;
            ++Version;
        }
    }
}
