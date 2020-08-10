using System;
using System.Collections.Generic;

namespace TastyBoutique.Persistance.Models
{
    public class User : Entity
    {
        public User(string username,string email,string password, string userType)
        {
            RecipeComment = new List<RecipeComment>();
            SavedRecipes = new List<SavedRecipes>();
            Username = username;
            Email = email;
            Password = password;
            UserType = userType;
            Status = "Active";
        }

        public Guid IdStudent { get; set; }
        public string UserType { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Password { get; set; }



        public virtual Student IdStudentNavigation { get; set; }
        public virtual ICollection<RecipeComment> RecipeComment { get; set; }
        public virtual ICollection<SavedRecipes> SavedRecipes { get; set; }
    }
}
