using System;
using System.Collections.Generic;

namespace TastyBoutique.Persistance.Models
{
    public partial class UserType : Entity
    {
        public UserType(string name, string description)
        {
            User = new HashSet<User>();
            this.Name = name;
            this.Description = description;
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
