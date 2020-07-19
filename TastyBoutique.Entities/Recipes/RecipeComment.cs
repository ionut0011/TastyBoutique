using System;

namespace TastyBoutique.Entities.Recipes
{
    public sealed class RecipeComment : Entity
    {
        public RecipeComment(string content, Guid userId) : base()
        {
            Content = content;
            UserId = userId;
        }

        public string Content { get; set; }

        public Guid UserId { get; set; }
    }
}