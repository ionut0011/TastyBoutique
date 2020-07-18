using System;
using System.Collections.Generic;

namespace TastyBoutique.Database.Models
{
    public partial class User
    {
        public User()
        {
            RecipeComment = new HashSet<RecipeComment>();
            SavedRecipes = new HashSet<SavedRecipes>();
        }

        public int Id { get; set; }
        public int IdStudent { get; set; }
        public int IdUserType { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Password { get; set; }

        public virtual Student IdStudentNavigation { get; set; }
        public virtual UserType IdUserTypeNavigation { get; set; }
        public virtual ICollection<RecipeComment> RecipeComment { get; set; }
        public virtual ICollection<SavedRecipes> SavedRecipes { get; set; }
    }
}
