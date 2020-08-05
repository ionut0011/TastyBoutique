using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace TastyBoutique.Persistance.Models
{
    public class Recipes : Entity
    {
        public Recipes(string name, Boolean access, string description, byte[] image)
        {
            Name = name;
            Access = access;
            Description = description;
            Image = image;
            NotificationsNavigation = new List<Notifications>();
            RecipeComment = new List<RecipeComment>();
            SavedRecipes = new List<SavedRecipes>();
            RecipesIngredients = new List<RecipesIngredients>();
            RecipesFilters = new List<RecipesFilters>();
        }
        public Guid IdUser { get; set; }
        public string Name { get; set; }
        public Boolean Access { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public virtual RecipeType RecipeType { get; set; }

        public ICollection<Notifications> NotificationsNavigation { get; set; }
        public ICollection<RecipeComment> RecipeComment { get; set; }
        public ICollection<SavedRecipes> SavedRecipes { get; set; }

        public ICollection<RecipesIngredients> RecipesIngredients { get; set; }

        public ICollection<RecipesFilters> RecipesFilters { get; set; }

        public void Update(string name, Boolean access, string description, byte[] image)
        {
            Name = name;
            Access = access;
            Description = description;
            Image = image;
        }

        public void AddComment(RecipeComment comment)
        {
            this.RecipeComment.Add(comment);
        }

        public void RemoveComment(Guid commentId)
        {
            var comment = this.RecipeComment.FirstOrDefault(c => c.Id == commentId);

            if (comment != null)
            {
                this.RecipeComment.Remove(comment);
            }
        }
    }
}
