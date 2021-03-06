﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TastyBoutique.Persistance.Models
{
    public class Recipes : Entity
    {
        public Recipes(string name, string type, bool access, string description, byte[] image)
        {
            //IdUser = id;
            Name = name;
            Type = type;
            Access = access;
            Description = description;
            Image = image;
            AverageReview = 0;
            ReviewCount = 0;
            RecipeComment = new List<RecipeComment>();
            SavedRecipes = new List<SavedRecipes>();
            Ingredients = new List<RecipesIngredients>();
            Filters = new List<RecipesFilters>();
            //Ingredients = new List<Ingredients>();
            //Filters = new List<Filters>();
        }
        public Guid IdUser { get; set; }
        public string Name { get; set; }
        public bool Access { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }

        public float AverageReview { get; set; }

        public int ReviewCount { get; set; }
        public string Type { get; set; }

       // public ICollection<Ingredients> Ingredients { get; set; }
       // public ICollection<Filters> Filters { get; set; }
        public ICollection<RecipeComment> RecipeComment { get; set; }
        public ICollection<SavedRecipes> SavedRecipes { get; set; }
       
        [IgnoreMap]
        public ICollection<RecipesIngredients> Ingredients { get; set; }

        public ICollection<RecipesFilters> Filters { get; set; }

        public void AddComment(RecipeComment comment)
        {
            this.RecipeComment.Add(comment);
            ReviewCount++;
            AverageReview = (AverageReview * (ReviewCount - 1) + comment.Review) / ReviewCount;
        }

        public void RemoveComment(Guid commentId)
        {
            var comment = this.RecipeComment.FirstOrDefault(c => c.Id == commentId);

            if (comment != null)
            {
                ReviewCount--;
                AverageReview = (AverageReview * (ReviewCount + 1) - comment.Review) / ReviewCount;
                this.RecipeComment.Remove(comment);
            }
        }
    }
}
