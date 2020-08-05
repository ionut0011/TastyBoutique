using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TastyBoutique.Business.Collections.Models
{
    public class SavedRecipeModel
    {
        public Guid IdRecipe { get; set; }

        [JsonIgnore]
        public Guid IdUser { get; set; }
    }
}
