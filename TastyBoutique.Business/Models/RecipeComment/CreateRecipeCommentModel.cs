using System;
using System.Text.Json.Serialization;

namespace TastyBoutique.Business.Models.RecipeComment
{
    public sealed class CreateRecipeCommentModel
    {

        [JsonIgnore]
        public Guid IdUser { get; set; }
        public string Comment { get; set; }
        public int Review { get; set; }
    }
}
