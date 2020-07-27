using System;
using System.Collections.Generic;

namespace TastyBoutique.Persistance.Models
{
    public partial class User : Entity
    {
        public User(string Username,string Email,string Password)
        {
            RecipeComment = new HashSet<RecipeComment>();
            SavedRecipes = new HashSet<SavedRecipes>();
            this.Username = Username;
            this.Email = Email;
            this.Password = Password;
        }

        public Guid IdStudent { get; set; }
        public Guid IdUserType { get; set; }
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
