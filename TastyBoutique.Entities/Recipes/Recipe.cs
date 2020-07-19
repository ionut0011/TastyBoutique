using System;
using System.Collections.Generic;
using System.Linq;
using TastyBoutique.Database.Models;

namespace TastyBoutique.Entities.Recipes
{
    public sealed class Recipe : Entity
    {
        public Recipe(string name, string description, bool @access, byte[] photoContent) : base()
        {
            Name = name;
            Description = description;
            Access = @access;
            PhotoContent = photoContent;
            RecipeComments = new List<RecipeComment>();
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public bool Access { get; private set; }

        public ICollection<RecipeComment> RecipeComments { get; private set; }

        public byte[] PhotoContent { get; private set; }

        public void AddComment(RecipeComment comment)
        {
            this.RecipeComments.Add(comment);
        }

        public void RemoveComment(Guid commentId)
        {
            var comment = this.RecipeComments.FirstOrDefault(c => c.Id == commentId);

            if (comment != null)
            {
                this.RecipeComments.Remove(comment);
            }
        }

        public void Update(string name, string description, bool @access, byte[] photocontent)
        {
            Name = name;
            Description = description;
            Access = @access;
            PhotoContent = photocontent;
        }
    }
}