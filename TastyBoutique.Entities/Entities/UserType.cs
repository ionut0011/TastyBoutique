using System;
using System.Collections.Generic;

namespace TastyBoutique.Persistance.Models
{
    public partial class UserType : Entity
    {
        public UserType(string type)
        {
            User = new HashSet<User>();
            this.Type = type;
           
        }

        public string Type { get; set; }
        
        public virtual ICollection<User> User { get; set; }
    }
}
