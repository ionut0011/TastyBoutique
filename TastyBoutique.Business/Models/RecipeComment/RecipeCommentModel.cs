using System;

namespace TastyBoutique.Business.Models.RecipeComment
{
    public sealed class RecipeCommentModel
    {
        public Guid IdRecipe { get; set; }
        public Guid IdUser { get; set; }
        public string Comment { get; set; }
        public int Review { get; set; }

    }
}
