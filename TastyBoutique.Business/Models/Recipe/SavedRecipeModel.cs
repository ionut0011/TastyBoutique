using System;
using System.Text.Json.Serialization;

namespace TastyBoutique.Business.Models.Recipe
{
    public class SavedRecipeModel
    {
        public Guid IdRecipe { get; set; }

        [JsonIgnore]
        public Guid IdUser { get; set; }
    }
}
