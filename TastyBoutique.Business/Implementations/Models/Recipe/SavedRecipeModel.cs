using System;
using System.Collections.Generic;
using System.Text;

namespace TastyBoutique.Business.Collections.Models
{
    public class SavedRecipeModel
    {
        public Guid IdRecipe { get; set; }
        public Guid IdUser { get; set; }
    }
}
