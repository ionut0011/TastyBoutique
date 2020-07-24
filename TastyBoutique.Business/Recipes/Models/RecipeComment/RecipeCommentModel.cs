using System;
using System.Collections.Generic;
using System.Text;

namespace TastyBoutique.Business.Recipes.Models.RecipeComment
{
    public sealed class RecipeCommentModel
    {
        public Guid IdRecipe { get; set; }
        public Guid IdUser { get; set; }
        public string Comment { get; set; }
        public string Review { get; set; }

    }
}
