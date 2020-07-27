using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TastyBoutique.Business.Recipes.Models.RecipeComment
{
    public sealed class CreateRecipeCommentModel
    {

        [JsonIgnore]
        public Guid IdUser { get; set; }
        public string Comment { get; set; }
        public string Review { get; set; }
    }
}
